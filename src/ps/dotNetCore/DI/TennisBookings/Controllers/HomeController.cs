using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TennisBookings.Caching;
using TennisBookings.Configuration;
using TennisBookings.Middleware;
using TennisBookings.Models;
using TennisBookings.Services;

namespace TennisBookings.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherForecaster _weatherForecaster;
        private readonly FeaturesConfiguration _featuresConfiguration;
      //  private readonly GuidGenerator _guidGenerator;
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache<WeatherResult> _cache;
        private SecondFeaturesConfiguration _secondFeatures;

        public HomeController(IWeatherForecaster weatherForecaster,
            IOptionsSnapshot<FeaturesConfiguration> options, // If we use IOptionsSnapshot, the configuration registered with scoped not singleton.
            //GuidGenerator guidGenerator,
            ILogger<HomeController> logger,
            IDistributedCache<WeatherResult> cache,
            IOptionsMonitor<SecondFeaturesConfiguration> secondFeatures)
        {
            _weatherForecaster = weatherForecaster;
            _featuresConfiguration = options.Value;
           // _guidGenerator = guidGenerator;
            _logger = logger;
            _cache = cache;
            _secondFeatures = secondFeatures.CurrentValue;

            // IOptionsMonitor also provide us OnChange() method.
            secondFeatures.OnChange(config =>
            {
                _secondFeatures = config;
                _logger.LogInformation("The greeting configuration has been updated.");
            });
        }
        public async Task<ViewResult> Index()
        {
           // _logger.LogInformation("Guid : {0}",_guidGenerator.Guid);

            var viewModel = new HomeViewModel();

            if (_featuresConfiguration.EnableWeatherForecast && _secondFeatures.EnableSecondWeatherForecast)
            {

                var cacheKey = $"current_weather_{DateTime.UtcNow:yyyy_MM_dd}";

                var (isCached, forecast) = await _cache.TryGetValueAsync(cacheKey);

                if (!isCached)
                {
                    forecast = _weatherForecaster.GetCurrentWeather();
                    await _cache.SetAsync(cacheKey, forecast, 60);
                }
                else
                {
                    _logger.LogInformation("Value is hit in cache.");
                }

                switch (forecast.WeatherCondition)
                {
                    case WeatherCondition.Sun:
                        viewModel.WeatherDescription = "It's sunny right now. " +
                                                       "A great day for tennis.";
                        break;
                    case WeatherCondition.Rain:
                        viewModel.WeatherDescription = "We're sorry but it's raining " +
                                                       "here. No outdoor courts in use.";
                        break;
                    default:
                        viewModel.WeatherDescription = "We don't have the latest weather " +
                                                       "information right now, please check again later.";
                        break;
                }
            }
            return View(viewModel);
        }
    }
}
