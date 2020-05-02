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
using TennisBookings.Configuration;
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
            services.AddTransient<IWeatherForecaster, WeatherForecaster>();
            services.AddTransient<IWeatherForecaster, AmazingWeatherForecaster>();  // The request is handled by last registered class. But two definition is registered the interface.
            services.TryAddTransient<IWeatherForecaster, AmazingWeatherForecaster>();  // If there is no registered service in interface, TryAdd() is ran.
            services.Replace(ServiceDescriptor.Transient<IWeatherForecaster, AmazingWeatherForecaster>()); // Its removed previous.
            // services.RemoveAll<IWeatherForecaster>();


            services.AddSingleton<INumberRules, CanNotBeLessThanZero>();
            services.AddSingleton<INumberRules, CanNotBeEqualToZero>();
            services.AddSingleton<INumberRules, CanNotBeGreaterThanTen>();
            // We can also use above function to avoid duplications for registered a service.
            //services.TryAddEnumerable(ServiceDescriptor.Singleton<INumberRules, CanNotBeGreaterThanTen>());

            //services.AddTransient<GuidGenerator>();
            //services.AddScoped<GuidGenerator>();
            //services.AddSingleton<GuidGenerator>();

            services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));

            services.AddControllersWithViews();
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
            
            //app.UseMiddleware<CustomMiddleware>();  // Middleware order is important. Custom middleware should be this order.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
