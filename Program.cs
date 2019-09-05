using System;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MacsASPNETCore.Models;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using static System.Environment;
using Microsoft.AspNetCore;

namespace MacsASPNETCore
{
    public class Program
    {
        private static IConfigurationRoot _configuration { get; set; }
        private static IHostingEnvironment _environment { get; }
        private static X509Certificate2 PfxCert { get; set; }
        private static AsymmetricAlgorithm PrivateKey { get; set; }

        public Program()
        {

        }



        public static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args);

            PfxCert = GetCertificate(_environment);

            host.UseStartup<Startup>()
            .UseKestrel(options =>
                {
                    switch (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                    {
                        case "Development":
                            options.Listen(IPAddress.Loopback, 8081);
                            options.Listen(IPAddress.Loopback, 8443,
                                listenOptions => { listenOptions.UseHttps(PfxCert); });
                            break;
                        case "Staging":
                            options.Listen(IPAddress.Any, 80);
                            options.Listen(IPAddress.Any, 443,
                              listenOptions => { listenOptions.UseHttps(PfxCert); });
                            break;
                        case "Production":
                            options.Listen(IPAddress.Any, 80);
                            options.Listen(IPAddress.Any, 443,
                              listenOptions => { listenOptions.UseHttps(PfxCert); });
                            break;
                    }
                })
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
                    .AddJsonFile("hosting.json", optional: false);

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

                config.AddEnvironmentVariables();
                _configuration = config.Build();
            });
            return builder;

        }

        private static X509Certificate2 GetCertificate(IHostingEnvironment environment)
        {
            var pfx = new X509Certificate2();
            switch (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
            {
                case "Development":

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
                            return pfx;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                var rawBytes = File.ReadAllBytes(certPath);
                                pfx = new X509Certificate2(rawBytes);
                                return pfx;
                            }
                            catch (CryptographicException ex)
                            {
                                Console.WriteLine($"Could not open certificate!\n\n{ex.Message}");
                                throw;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Another error occurred, see exception details");
                                Console.WriteLine(ex.Message);
                                throw;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(certPath + " does not exist or is invalid!");
                        var files = Directory.GetFiles(Directory.GetCurrentDirectory().ToString());
                        foreach (var item in files)
                        {
                            Console.WriteLine(item.ToString());
                        }

                        Exit(4);
                    }

                    break;
                default:
                    pfx = GetKeyVaultCert().Result ?? throw new ArgumentNullException($"GetKeyVaultCert().Result");
                    return pfx;
            }
            return new X509Certificate2();


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
                if (secret.ContentType == "application/x-pkcs12")
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

            if (result == null)
                throw new InvalidOperationException("Failed to get Azure JWT Token");

            return result.AccessToken;
        }
    }


}
