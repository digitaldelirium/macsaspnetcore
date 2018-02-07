using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using MacsASPNETCore.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using MacsASPNETCore.Services;
using Microsoft.Extensions.Logging;
using MacsASPNETCore.ViewModels;
using AutoMapper;
using AutoMapper.Mappers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace MacsASPNETCore
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        private IHostingEnvironment Environment { get; }
        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder();
            Environment = environment;

            builder.SetBasePath(Directory.GetCurrentDirectory());

            if (Environment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
                builder.AddJsonFile("appsettings.Development.json", optional: false);
            }
            else if (Environment.IsStaging())
            {
                builder.AddJsonFile("appsettings.Staging.json", optional: false);
            }
            else {
                builder.AddJsonFile("appsetttings.json", optional: false);
            }

            builder.AddJsonFile("hosting.json", optional: false);
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();

            var vaultName = Configuration["Azure:KeyVault:Vault"];
            builder.AddAzureKeyVault(
                $"https://{vaultName}.vault.azure.net",
                Configuration["Azure:KeyVault:ClientId"],
                Configuration["Azure:KeyVault:ClientSecret"]);
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var activities = Configuration["Data:ActivityDb"];
            var appdb = Configuration["Data:ApplicationDb"];
            var customerdb = Configuration["Data:CustomerDb"];
            var rezdb = Configuration["Data:ReservationDb"];

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<ActivityDbContext>(options => options.UseSqlite(activities))
                    .AddDbContext<CustomerDbContext>(options => options.UseSqlite(customerdb))
                    .AddDbContext<ReservationDbContext>(options => options.UseSqlite(rezdb))
                    .AddDbContext<ApplicationDbContext>(options => options.UseSqlite(appdb));
            }
            else
            {

                services.AddDbContext<ActivityDbContext>(options => options.UseMySql(activities))
                    .AddDbContext<CustomerDbContext>(options => options.UseMySql(customerdb))
                    .AddDbContext<ReservationDbContext>(options => options.UseMySql(rezdb))
                    .AddDbContext<ApplicationDbContext>(options => options.UseMySql(appdb));

            }

            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.SignIn.RequireConfirmedEmail = true;
                    // Each user must have a unique email
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IConfiguration>(Configuration);
            
            // Require HTTPS
            services.Configure<MvcOptions>(options => { options.Filters.Add(new RequireHttpsAttribute()); });
            
            services.AddMvc()
                .AddJsonOptions(
                    opt => { opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); }
                );
            services.AddLogging();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["FacebookAppId"];
                facebookOptions.AppSecret = Configuration["FacebookAppSecret"];
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MacsCampingArea";
                options.Cookie.HttpOnly = false;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });
#if DEBUG
            //Add Email Service
            services.AddScoped<IEmailSender, DebugMailService>();
#else
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
#endif
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var options = new RewriteOptions()
                .AddRedirectToHttps();
            app.UseRewriter(options);

            CreateRoles(serviceProvider).Wait();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {

                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                            .Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            app.UseStaticFiles();

            Mapper.Initialize(config => { config.CreateMap<Activity, ActivityViewModel>().ReverseMap(); }
                );

            app.UseAuthentication();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleMgr = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var usrMgr = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleNames = new string[]
            {
                "Administrators",
                "Managers",
                "Users",
                "Guests"
            };
            IdentityResult roleResult;
            
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleMgr.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleMgr.CreateAsync(new ApplicationRole(roleName));
                }
            }

            var adminUser = new ApplicationUser
            {
                UserName = "Administrator",
                Email = Configuration["AppSettings:AdminEmail"],
                EmailConfirmed = true
            };

            string adminPwd = Configuration["AppSettings:AdminPassword"];
            var user = await usrMgr.FindByEmailAsync(Configuration["AppSettings:AdminEmail"]);

            if (user == null)
            {
                var createAdmin = await usrMgr.CreateAsync(adminUser, adminPwd);
                if (createAdmin.Succeeded)
                {
                    await usrMgr.AddToRoleAsync(adminUser, "Administrators");
                }
            }
        }
    }
}

