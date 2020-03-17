using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exCoreMvc.Data;
using exCoreMvc.HostedServices;
using exCoreMvc.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace exCoreMvc
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
            services.AddControllersWithViews();
            services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.Configure<ConfigurationData>(c => Configuration.GetSection("Data").Bind(c));
            services.AddHostedService<HelloWorldHostedService>();
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

            app.Use(async (context,next)=>
            {
                Log.Information("Default Use Middleware.");
                await next.Invoke();
            });

            app.Map("/mapExample",
                b => b.Use(async (context, next) => { await context.Response.WriteAsync("MapExample middleware."); }));

            //UseWhen
            app.MapWhen(c=>c.Request.Query.ContainsKey("mapValue"),
                b => b.Use(async (context, next) => { await context.Response.WriteAsync($"Middleware with value={context.Request.Query["mapValue"]}."); }));

            app.UseWhen(b => b.Request.Path == new PathString("/daisy"), c => c.UseMiddleware<DaisyMiddleware>());

            app.UseWhen(b => b.Request.Path == new PathString("/configuration"), c => c.UseMiddleware<ConfigurationMiddleware>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

/* //Run method breaks the chain. We need to invoke next middleware
            app.Run(async context =>
            {
                Log.Information("Default Run Middleware.");
            });
*/
        }
    }
}
