using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TennisBookings.Services;

namespace TennisBookings.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;
        //private readonly IWeatherForecaster _weatherForecaster;

        public CustomMiddleware(RequestDelegate next,
            ILogger<CustomMiddleware> logger)
            // ,IWeatherForecaster weatherForecaster )
        {
            _next = next;
            _logger = logger;
            //_weatherForecaster = weatherForecaster;
        }

        // We shouldn't inject short life services (scoped,transient) via constructor in middleware.
        // We use middleware injection with Invoke() or InvokeAsync() methods.
        public async Task InvokeAsync(HttpContext context, GuidGenerator guid, IWeatherForecaster weatherForecaster) // Middleware Injection
        {
            var sky = weatherForecaster.GetCurrentWeather().WeatherCondition;
            _logger.LogInformation("Guid : {0} and Sky will be {1}", guid.Guid, sky);
            _next.Invoke(context);
        }
    }
}
