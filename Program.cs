using System;
using Microsoft.AspNetCore.Hosting;
using MacsASPNETCore;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Reflection;
using System.Security;
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
        private static IHostingEnvironment Environment { get; set; }
        private static X509Certificate2 PfxCert { get; set; }
        
        public Program(IHostingEnvironment environment)
        {
            Environment = environment;
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
                    logger.LogError(e, "An error occurred seeding the Activity DB.");
                    throw;
                }
            }
            
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = new WebHostBuilder();

            var host = builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("hosting.json", optional: false)
                        .AddEnvironmentVariables();

                    if (Environment.IsDevelopment())
                    {
                        config.AddJsonFile("appsettings.Development.json", optional: false);
                    }
                    else if (Environment.IsStaging())
                    {
                        config.AddJsonFile("appsettings.Staging.json", optional: false);
                    }
                    else
                    {
                        config.AddJsonFile("appsettings.json", optional: false);
                    }
                    
                    _configuration = config.Build();

                });

                PfxCert = GetCertificate(Environment);

                host.UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 80);
                    options.Listen(IPAddress.Loopback, 443,
                        listenOptions => { listenOptions.UseHttps(PfxCert); });
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseApplicationInsights();

            return host.Build();
        }

        private static X509Certificate2 GetCertificate(IHostingEnvironment environment)
        {
            var pfx = new X509Certificate2();
            if (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                string certPath = Directory.GetCurrentDirectory().ToString() + "/Macs-Dev.pfx";
                if (File.Exists(certPath))
                {
                    try
                    {
                        var rawBytes = File.ReadAllBytes(certPath);
                        var securePassword = new SecureString();
                        foreach (var c in "developer")
                        {
                            securePassword.AppendChar(c);
                        }
                        pfx = new X509Certificate2(rawBytes, securePassword);
                    }
                    catch (Exception exception)
                    {
                        try {
                            var rawBytes = File.ReadAllBytes(certPath);
                            pfx = new X509Certificate2(rawBytes);
                        }
                        catch (CryptographicException ex){
                            Console.WriteLine($"Could not open certificate!\n\n{ex.Message}");
                            Exit(5);
                        }
                        catch (Exception ex){
                            Console.WriteLine("Another error occurred, see exception details");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                            Exit(6);
                        }
                    }
                }
                else
                {
                    Console.WriteLine(certPath + " is invalid!");
                    Exit(4);
                }
      
            }
            else if (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Staging"){
                var certPath = Directory.GetCurrentDirectory().ToString() + "/Macs.pfx";
                
                if(!File.Exists(certPath)){
                    try {
                        var rawBytes = Encoding.ASCII.GetBytes(_configuration["Certificates:MacsVM:PFX"]);
                        pfx = new X509Certificate2(rawBytes);
                    }
                    catch (CryptographicException exception)
                    {
                        Console.WriteLine($"Could not open certificate!\n\n{exception.Message}");
                        throw;
                    }
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
            try
            {
                var kvClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
                var certBundle = await kvClient
                    .GetCertificateAsync("https://macscampvault.vault.azure.net/certificates/macsvmssl");
                
                pfx = new X509Certificate2(certBundle.Cer);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a problem during the key vault operation\n{ex.Message}");
                throw;
            }

            return pfx;
        }

        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            Console.WriteLine($"ClientId = 44c4e2a1-4b32-4d7b-b063-ab00907ab449");
            Console.WriteLine($"ClientSecret = #{client-secret}#");
            var clientCredential = new ClientCredential("44c4e2a1-4b32-4d7b-b063-ab00907ab449", 
                "#{client-secret}#");

            var result = await authContext.AcquireTokenAsync(resource, clientCredential);
            
            if(result == null)
                throw new InvalidOperationException("Failed to get Azure JWT Token");

            return result.AccessToken;
        }
    }
}
