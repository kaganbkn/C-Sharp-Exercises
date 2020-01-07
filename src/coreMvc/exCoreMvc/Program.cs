using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exCoreMvc.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace exCoreMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger=new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(configuration.GetValue<string>("LogFile"))
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();
            //service lifetime --> Singleton,Scoped,Transient
            using (var scope=host.Services.CreateScope()) //how is it working?
            {
                var service = scope.ServiceProvider;
                try
                {
                    SeedData.Initialize(service);
                    Log.Information("Seed is worked from main.");
                }
                catch (Exception e)
                {
                    Log.Information("Seed is not worked from main.");
                }
            }

            try
            {
                Log.Information("Starting up");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
