using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        public HomeController(IWeatherForecaster weatherForecaster,
            IOptions<FeaturesConfiguration> options, 
            //GuidGenerator guidGenerator,
            ILogger<HomeController> logger)
        {
            _weatherForecaster = weatherForecaster;
            _featuresConfiguration = options.Value;
           // _guidGenerator = guidGenerator;
            _logger = logger;
        }
        public IActionResult Index()
        {
           // _logger.LogInformation("Guid : {0}",_guidGenerator.Guid);

            var viewModel = new HomeViewModel();

            if (_featuresConfiguration.EnableWeatherForecast)
            {
                var currentWeather = _weatherForecaster.GetCurrentWeather();

                switch (currentWeather.WeatherCondition)
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
