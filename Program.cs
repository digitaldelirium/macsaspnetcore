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
        private static AsymmetricAlgorithm PrivateKey { get; set; }
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
                    
                    _configuration = config.Build();

                });

                PfxCert = GetCertificate(Environment);
                PrivateKey = PfxCert.GetRSAPrivateKey();

                host.UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    if (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                    {
                        options.Listen(IPAddress.Loopback, 8081);
                        options.Listen(IPAddress.Loopback, 8443,
                            listenOptions => { listenOptions.UseHttps(PfxCert); });
                    }
                    else
                    {
                        options.Listen(IPAddress.Any, 80);
                        options.Listen(IPAddress.Any, 443,
                            listenOptions => { listenOptions.UseHttps(PfxCert); });
                    }
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
                var certPath = Directory.GetCurrentDirectory().ToString() + "/Macs-Dev.pfx";
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
                    catch (Exception)
                    {
                        try {
                            var rawBytes = File.ReadAllBytes(certPath);
                            pfx = new X509Certificate2(rawBytes);
                        }
                        catch (CryptographicException ex){
                            Console.WriteLine($"Could not open certificate!\n\n{ex.Message}");
                            throw;
                        }
                        catch (Exception ex){
                            Console.WriteLine("Another error occurred, see exception details");
                            Console.WriteLine(ex.Message);
                            throw;
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
            else
            {
                pfx = GetKeyVaultCert().Result ?? throw new ArgumentNullException("GetKeyVaultCert().Result");
            }

            return pfx;
        }

        public static async Task<X509Certificate2> GetKeyVaultCert()
        {
            X509Certificate2 pfx;

            try
            {
                var kvClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
                var secret = await kvClient
                    .GetSecretAsync("https://macscampvault.vault.azure.net/secrets/macsvmssl").ConfigureAwait(false);

                byte[] bytes;
                if(secret.ContentType == "application/x-pkcs12")
                    bytes = Convert.FromBase64String(secret.Value);
                else
                {
                    bytes = new byte[0];
                    Console.WriteLine("secret is not PFX!!");
                    throw new ArgumentException("This is not a PFX string!!");
                }
                var password = new SecureString();
                
                var coll = new X509Certificate2Collection();
                coll.Import(bytes, null, X509KeyStorageFlags.Exportable);
                pfx = coll[0];
                File.WriteAllBytes(Directory.GetCurrentDirectory().ToString() + "/Macs.pfx", bytes);
                Console.WriteLine(pfx.HasPrivateKey);
                Console.WriteLine(pfx.GetRSAPrivateKey());

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
            
            var clientCredential = new ClientCredential("e8725941-c27a-4012-8c89-19aca10b11a5", 
                "#{client-secret}#");

            var result = await authContext.AcquireTokenAsync(resource, clientCredential);
            
            if(result == null)
                throw new InvalidOperationException("Failed to get Azure JWT Token");

            return result.AccessToken;
        }
    }
}
