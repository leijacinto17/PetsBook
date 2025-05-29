using System.Text;
using Core.Entities.Authorization;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;

namespace PetsBook.API.Extensions.ServiceExtension
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders(["Location", "Upload-Offset", "Operation-Location"])
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials());
            });
        }

        public static void ConfigureIISIntergration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureIdentityFramework(this IServiceCollection services)
        {
            //Identity Framework
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                //Lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // TokenProvider
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            //.AddUserStore<User>()
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //Authentication
            services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("site"),
                    ValidAudience = Environment.GetEnvironmentVariable("audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("secret"))),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void ConfigureDataBaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Infrastructure")));
        }

        public static void ConfigureTokenTimeSpan(this IServiceCollection services)
        {
            //Token TimeSpan
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(1);
            });
        }

        public static void ConfigureEnvironmentVariables(this IServiceCollection services, IConfiguration configuration)
        {
            // AppSettings for authentication and JWT
            var appSettingSection = configuration.GetSection("Security");
            Environment.SetEnvironmentVariable("site", appSettingSection.GetValue<string>("Site"));
            Environment.SetEnvironmentVariable("secret", appSettingSection.GetValue<string>("Secret"));
            Environment.SetEnvironmentVariable("expireTime", appSettingSection.GetValue<string>("ExpireTime"));
            Environment.SetEnvironmentVariable("refreshExpireTime", appSettingSection.GetValue<string>("RefreshExpireTime"));
            Environment.SetEnvironmentVariable("minusMinutes", appSettingSection.GetValue<string>("MinusMinutes"));
            Environment.SetEnvironmentVariable("audience", appSettingSection.GetValue<string>("Audience"));
        }


    }
}
