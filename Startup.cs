using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MacsASPNETCore.Models;
using MacsASPNETCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MacsASPNETCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;

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
                            "https://connect.facebook.com"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    }
                );
            });

            if (Environment.EnvironmentName == "Development")
            {
                services.AddDbContext<ActivityDbContext>(options => options.UseSqlite(activities))
                    .AddDbContext<CustomerDbContext>(options => options.UseSqlite(customerDb))
                    .AddDbContext<ReservationDbContext>(options => options.UseSqlite(rezdb))
                    .AddDbContext<ApplicationDbContext>(options => options.UseSqlite(appdb));
            }
            else
            {
                services.AddDbContext<ActivityDbContext>(options => options.UseMySql(activities))
                    .AddDbContext<CustomerDbContext>(options => options.UseMySql(customerDb))
                    .AddDbContext<ReservationDbContext>(options => options.UseMySql(rezdb))
                    .AddDbContext<ApplicationDbContext>(options => options.UseMySql(appdb));
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
