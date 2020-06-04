using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TennisBookings.BackgroundServices;
using TennisBookings.Caching;
using TennisBookings.Configuration;
using TennisBookings.DependencyInjection;
using TennisBookings.Middleware;
using TennisBookings.Rules;
using TennisBookings.Services;

namespace TennisBookings
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

            services.AddAppConfiguration(Configuration)
                .AddWeatherService()
                .AddNumberRuleService()
                .AddCachingService()
                .AddGuidService();

            services.AddHostedService<ValidateOptionsService>(); // We add this because we don't want to run app if configuration is fault.

            services.AddControllersWithViews();

            services.AddHostedService<DegreeCacheService>();

            /*
            services.TryAddSingleton<CanNotBeGreaterThanTen>();
            services.AddTransient<IIncreaseNumber>(sp => sp.GetRequiredService<CanNotBeGreaterThanTen>());
            services.AddTransient<INumberRules>(sp => sp.GetRequiredService<CanNotBeGreaterThanTen>());
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseMiddleware<CustomMiddleware>();  // Middleware order is important. Custom middleware should be this order.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
