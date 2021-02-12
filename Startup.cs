using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MacsASPNETCore.Models;
using MacsASPNETCore.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;

namespace MacsASPNETCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;

        }

        private IConfigurationRoot GetConfigurationRoot()
        {
            const string environmentPrefix = "CONFIG_";
            return new ConfigurationBuilder()
                .AddEnvironmentVariables(environmentPrefix).Build();
        }
        public IConfiguration Configuration { get; private set; }
        public IHostingEnvironment Environment { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var activities = Configuration.GetValue<String>("Data:ActivityDb");
            var appdb = Configuration.GetValue<String>("Data:ApplicationDb");
            var customerDb = Configuration.GetValue<String>("Data:CustomerDb");
            var rezdb = Configuration.GetValue<String>("Data:ReservationDb");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(
                            "https://macscampingarea.com",
                            "http://macscampingarea.com",
                            "https://www.macscampingarea.com",
                            "http://www.macscampingarea.com",
                            "https://connect.facebook.com",
                            "https://macscampingarea.azurewebsites.net",
                            "http://macscampingarea.azurewebsites.net"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    }
                );
            });

            if (Environment.EnvironmentName == "Development")
            {
                services.AddDbContext<ActivityDbContext>(options => options.UseSqlite(activities));
                // .AddDbContext<CustomerDbContext>(options => options.UseSqlite(customerDb))
                // .AddDbContext<ReservationDbContext>(options => options.UseSqlite(rezdb))
                // .AddDbContext<ApplicationDbContext>(options => options.UseSqlite(appdb));
            }
            else
            {
                activities = System.Environment.GetEnvironmentVariable("CUSTOMCONNSTR_DELIRIUMDBACTIVITIES");
                services.AddDbContext<ActivityDbContext>(options => options.UseMySql(activities));
                // .AddDbContext<CustomerDbContext>(options => options.UseMySql(customerDb))
                // .AddDbContext<ReservationDbContext>(options => options.UseMySql(rezdb))
                // .AddDbContext<ApplicationDbContext>(options => options.UseMySql(appdb));          
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                // Azure Subnets
                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:10.0.0.0"), 104));
                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:192.168.0.0"), 112));
                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:172.16.0.0"), 108));
            });
            services.AddNodeServices();
            services.AddMvcCore()
                .AddRazorViewEngine();

            services.AddScoped<IEmailSender, AuthMessageSender>();
            services.AddScoped<IActivityRepository, ActivityRepository>();

            services.AddApplicationInsightsTelemetry(Configuration.GetValue<String>("ApplicationInsights:InstrumentationKey"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseForwardedHeaders();
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
