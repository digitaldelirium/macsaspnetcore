using System;
using Microsoft.AspNetCore.Hosting;
using MacsASPNETCore;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MacsASPNETCore.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
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

                _pfxCert = GetCertificate(_environment);

                host.UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 5000);
                    options.Listen(IPAddress.Loopback, 5001,
                        listenOptions => { listenOptions.UseHttps(_pfxCert); });
                })
                .UseIISIntegration()
                .UseApplicationInsights();

            return host.Build();
        }

        private static X509Certificate2 GetCertificate(IHostingEnvironment environment)
        {
            var pfx = new X509Certificate2();
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                string certPath = Directory.GetCurrentDirectory().ToString() + "/Macs.pfx";
                if (File.Exists(certPath))
                {
                    try
                    {
                        var rawBytes = File.ReadAllBytes(certPath);
                        pfx = new X509Certificate2(rawBytes);
                    }
                    catch (CryptographicException exception)
                    {
                        Console.WriteLine($"Could not open certificate!\n\n{exception.Message}");
                        Exit(5);
                    }
                }
                else
                {
                    Console.WriteLine(certPath + " is invalid!");
                    Exit(4);
                }
      
            }
            else
            {
                var keyVaultCert = GetKeyVaultCert().Result ?? throw new ArgumentNullException("GetKeyVaultCert().Result");
                pfx = new X509Certificate2(keyVaultCert.RawData);
            }

            return pfx;
        }

        public static async Task<X509Certificate2> GetKeyVaultCert()
        {
            var pfx = new X509Certificate2();
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            try
            {
                var kvClient = new KeyVaultClient(
                    new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

                var secret = await kvClient
                    .GetSecretAsync(
                        "https://macscampvault.vault.azure.net/secrets/MacsPFX/9c7146c9e9a54b6b93fb232109d7de07")
                    .ConfigureAwait(false);

                if (secret.ContentType.Equals("application/x-pkcs12"))
                {
                    byte[] rawBytes = Encoding.ASCII.GetBytes(secret.Value);
                    pfx = new X509Certificate2(rawBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a problem during the key vault operation\n{ex.Message}");
                Exit(6);
            }

            return pfx;
        }
    }
}
