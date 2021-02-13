using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MacsASPNETCore
{
    public class Program
    {
        private static IConfigurationRoot _configuration { get; set; }
        private static IHostingEnvironment _environment { get; }

        public Program()
        {

        }



        public static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args);

            host.UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseApplicationInsights()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                });

            host.Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = new WebHostBuilder();
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("hosting.json", optional: true);

                if (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    config.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
                }
                else
                {
                    var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true);
                }

                var settings = config.Build();
                var connection = settings.GetConnectionString("AppConfig");
                config.AddAzureAppConfiguration(connection);
                config.AddEnvironmentVariables();
                _configuration = config.Build();
            });
            return builder;

        }
    }


}
