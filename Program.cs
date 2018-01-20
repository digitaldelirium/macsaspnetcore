using System;
using Microsoft.AspNetCore.Hosting;
using MacsASPNETCore;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MacsASPNETCore.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.Azure.KeyVault;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Remotion.Linq.Clauses.ResultOperators;
using SQLitePCL;
using static System.Environment;

namespace MacsASPNETCore
{
    public class Program
    {
        private static IConfigurationRoot _configuration;
        private static IHostingEnvironment _environment { get; set; }
        private static X509Certificate2 _pfxCert { get; set; }
        
        public Program(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        
        public static void Main(string[] args)
        {
            
            BuildWebHost(args).Run();
            
 /*           using (var scope = host.Services.CreateScope())
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
            } */
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = new WebHostBuilder();

            var host = builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("hosting.json", optional: false)
                        .AddEnvironmentVariables();

                    _configuration = config.Build();

                });

            var cert = GetCertificate(_environment);
            if (!File.Exists(certPath))
            {
                Console.WriteLine(certPath);
                Exit(4);
            }

                host.UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 5000);
                    options.Listen(IPAddress.Loopback, 5001,
                        listenOptions => { listenOptions.UseHttps(certPath, null); });
                })
                .UseIISIntegration()
                .UseApplicationInsights();

            return host.Build();
        }

        private static object GetCertificate(IHostingEnvironment environment)
        {
            var pfx = new X509Certificate2();
            if (environment.IsDevelopment())
            {
                string certPath = Directory.GetCurrentDirectory().ToString() + "Macs.pfx";
                pfx = new X509Certificate2(certPath);
            }
            else
            {
                
            }
        }

        public static async Task<string> GetToken(string authority, string res, string scope)
        {
            var auth = new AuthenticationContext(authority);
            var cred = new ClientCredential(WebConfigurationManager.AppSettings["ClientId],
                WebConfigurationManager.AppSettings["ClientSecret"]);
            
        }
    }
}
