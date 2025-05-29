using System.Reflection;
using Infrastructure.Data;
using Infrastructure.Data.SeedData;
using Microsoft.EntityFrameworkCore;

namespace PetsBook.API
{
    public class Program
    {
        protected Program()
        {

        }

        public async static Task Main(string[] args)
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    await context.Database.MigrateAsync();
                    await DataInitializer.Seed(services); //<---Do your seeding here
                }
                catch (Exception ex)
                {

                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Critical);
                    });
                });
    }
}
