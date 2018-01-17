using Microsoft.AspNetCore.Hosting;
using System;
using MacsASPNETCore;
using System.IO;
using System.Reflection;
using MacsASPNETCore.Models;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MacsASPNETCore
{
    public class Program
    {
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
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())                
//                .UseContentRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
    }
}
