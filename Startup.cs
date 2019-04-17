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
using macsaspnetcore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace macsaspnetcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var activities = Configuration["Data:ActivityDb"];
            var appdb = Configuration["Data:ApplicationDb"];
            var customerDb = Configuration["Data:CustomerDb"];
            var rezdb = Configuration["Data:ReservationDb"];

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            /*            services.AddDbContext<ApplicationDbContext>(options =>
                            options.UseSqlite(
                                Configuration.GetConnectionString("DefaultConnection")));
                        services.AddDefaultIdentity<IdentityUser>()
                            .AddEntityFrameworkStores<ApplicationDbContext>(); */

#if DEBUG
            services.AddDbContext<ActivityDbContext>(options => options.UseSqlite(activities))
                .AddDbContext<CustomerDbContext>(options => options.UseSqlite(customerDb));
#else
            services.AddDbContext<ActivityDbContext>(options => options.UseMySql(activities))
                .AddDbContext<CustomerDbContext>(options => options.UseMySql(customerDb))
                .AddDbContext<ReservationDbContext>(options => options.UseMySql(rezdb))
                .AddDbContext<ApplicationDbContext>(options => options.UseMySql(appdb));
#endif


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddNodeServices();
            services.AddMvcCore()
                .AddRazorViewEngine();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
