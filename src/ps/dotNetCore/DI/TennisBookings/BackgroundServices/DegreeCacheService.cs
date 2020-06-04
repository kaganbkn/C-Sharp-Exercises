using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TennisBookings.Caching;
using TennisBookings.Services;

namespace TennisBookings.BackgroundServices
{
    public class DegreeCacheService : BackgroundService
    {
        //Our dependencies must be singleton. 
        private readonly IWeatherForecaster _weatherForecaster;
        private readonly IDistributedCache<WeatherResult> _cache;
        private readonly ILogger<DegreeCacheService> _logger;
        private readonly int secondsToCache = 10;

        public DegreeCacheService(IWeatherForecaster weatherForecaster,
            IDistributedCache<WeatherResult> cache,
            ILogger<DegreeCacheService> logger)
        {
            _weatherForecaster = weatherForecaster;
            _cache = cache;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var degree = _weatherForecaster.GetCurrentDegree();
                await _cache.SetAsync("degree", new WeatherResult { Degree = degree }, secondsToCache);
                _logger.LogInformation($"Degree is cached with {degree}");
                await Task.Delay(TimeSpan.FromSeconds(secondsToCache - 1), stoppingToken);
            }
        }
    }
}
