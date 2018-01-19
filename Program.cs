using Microsoft.AspNetCore.Hosting;
using System;
using MacsASPNETCore;
using System.IO;
using System.Net;
using System.Reflection;
using MacsASPNETCore.Models;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MacsASPNETCore
{
    public class Program
    {
        private static IConfiguration Configuration { get; set; }

        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);


            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ActivityDbContext>();
                    ActivityDbContextSeedData.Initialize(context);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occurred seeding the DB.");
                    throw;
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 5000);
                    options.Listen(IPAddress.Loopback, 5001, listenOptions =>
                        {
                            listenOptions.UseHttps("Macs.pfx", "Yb8b924VXG8S");
                        });
                })
                .UseIISIntegration()
                .UseApplicationInsights()
                .Build();
    }
}
