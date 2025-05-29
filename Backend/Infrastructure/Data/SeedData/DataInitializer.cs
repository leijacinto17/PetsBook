using System.Diagnostics;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Entities.Enums.Roles;
using Entities.Models;
using Entities.Models.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Infrastructure.Data.SeedData
{
    public class DataInitializer
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            // Get DataContext Service
            var context = serviceProvider.GetRequiredService<IUnitOfWork>();

            //Get Usermanager Service
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Check if database contains anything
            // If not than seed data
            if (context.User.CheckAnyUsers())
            {
                return; // Already seeded
            }

            await CreateRoles(serviceProvider);
            await CreateFakeUsersAsync(serviceProvider, userManager);


        }

        public async static Task CreateRoles(IServiceProvider serviceProvider)
        {
            RoleManager<Role> roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            string[] roles = [
                RoleType.User.ToString(),
                RoleType.Admin.ToString(),
                RoleType.SuperAdmin.ToString(),
            ];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    try
                    {
                        var roleName = new Role()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = role,
                        };
                        await roleManager.CreateAsync(roleName);
                    }
                    catch 
                    {
                        throw;
                    }
                }
            }
        }

        public async static Task CreateFakeUsersAsync(IServiceProvider serviceProvider, UserManager<User> userManager)
        {
            //check dev user if existing
            var devUser = await userManager.FindByEmailAsync("devuser@development.com");
            if (devUser == null)
            {
                try
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "devuser@development.com",
                        Email = "devuser@development.com",
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, "dev@userP@ssw0rd");

                    if (result.Succeeded)
                    {
                        // Add role for super admin
                        user.AddRole(userManager, RoleType.SuperAdmin.ToString());

                        Debug.WriteLine("User created successfully");
                    }
                    else
                    {
                        Debug.WriteLine("Error creating user");
                        foreach (var error in result.Errors)
                        {
                            Debug.WriteLine($" - {error}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}
