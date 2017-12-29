using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MacsASPNETCore.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using MacsASPNETCore.Services;
using Microsoft.Extensions.Logging;
using MacsASPNETCore.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace MacsASPNETCore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var activities = Configuration["Data:ActivityDb:ConnectionString"];
            var appdb = Configuration["Data:ApplicationDb:ConnectionString"];
            var customerdb = Configuration["Data:CustomerDb:ConnectionString"];
            var rezdb = Configuration["Data:ReservationDb:ConnectionString"];
            
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddDbContext<ActivityDbContext>(options => options.UseMySQL(activities))
                .AddDbContext<CustomerDbContext>(options => options.UseMySQL(customerdb))
                .AddDbContext<ReservationDbContext>(options => options.UseMySQL(rezdb))
                .AddDbContext<ApplicationDbContext>(options => options.UseMySQL(appdb));
            
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); 
            services.AddMvc()
                .AddJsonOptions(
                    opt => { opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); }
                );
            services.AddLogging();
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


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

        // Entry point for the application.
        //public static void Main(string[] args) => WebHostBuilder.Run<Startup>(args);
    }
}

