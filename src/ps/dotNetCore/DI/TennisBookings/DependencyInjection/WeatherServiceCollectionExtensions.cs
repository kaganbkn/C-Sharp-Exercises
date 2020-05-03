using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Services;

namespace TennisBookings.DependencyInjection
{
    public static class WeatherServiceCollectionExtensions
    {
        public static IServiceCollection AddWeatherService(this IServiceCollection services)
        {

            services.AddTransient<IWeatherForecaster, WeatherForecaster>();
            services.AddTransient<IWeatherForecaster, AmazingWeatherForecaster>();  // The request is handled by last registered class. But two definition is registered the interface.
            services.TryAddTransient<IWeatherForecaster, AmazingWeatherForecaster>();  // If there is no registered service in interface, TryAdd() is ran.
            services.Replace(ServiceDescriptor.Transient<IWeatherForecaster, WeatherForecaster>()); // Its removed previous.
            services.RemoveAll<IWeatherForecaster>();

            services.AddScoped<IWeatherForecaster, WeatherForecaster>();

            return services;
        }
    }
}
