﻿using Microsoft.AspNetCore.Builder;
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

namespace MacsASPNETCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var activities = Configuration["Data:ActivityDb:ConnectionString"];
            // Add framework services.
            //services.AddApplicationInsightsTelemetry(Configuration);
            services.AddDbContext<ActivityDbContext>(options => options.UseSqlServer(activities))
                .AddDbContext<CustomerDbContext>()
                .AddDbContext<ReservationDbContext>()
                .AddDbContext<ApplicationDbContext>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
 /*           services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); */
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

            //app.UseApplicationInsightsRequestTelemetry();

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
                catch
                {
                }
            }

            //app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            Mapper.Initialize(config => { config.CreateMap<Activity, ActivityViewModel>().ReverseMap(); }
                );

            //app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });
        }

        // Entry point for the application.
        //public static void Main(string[] args) => WebHostBuilder.Run<Startup>(args);
    }
}
