using PetsBook.API.Extensions.DataInitializer;
using System.Reflection;

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

                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
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
