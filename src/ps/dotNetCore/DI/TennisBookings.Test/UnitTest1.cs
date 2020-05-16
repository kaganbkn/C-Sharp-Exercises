using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TennisBookings.Caching;
using TennisBookings.Configuration;
using TennisBookings.Controllers;
using TennisBookings.Models;
using TennisBookings.Services;
using Xunit;

namespace TennisBookings.Test
{
    public class UnitTest1
    {
        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsSun()
        {

            var mockWeatherForecaster = new Mock<IWeatherForecaster>();
            mockWeatherForecaster.Setup(c => c.GetCurrentWeather()).Returns(new WeatherResult
            {
                WeatherCondition = WeatherCondition.Sun
            });
            //var featuresOptions = Options.Create(new FeaturesConfiguration {EnableWeatherForecast = true});
            var featuresOptions = new Mock<IOptionsSnapshot<FeaturesConfiguration>>();
            featuresOptions.SetupGet(c => c.Value)
                .Returns(new FeaturesConfiguration { EnableWeatherForecast = true });
            var weatherResult = new Mock<IDistributedCache<WeatherResult>>();
            weatherResult.Setup(c => c.TryGetValueAsync(It.IsAny<string>()))
                .ReturnsAsync((false, new WeatherResult
                {
                    WeatherCondition = It.IsAny<WeatherCondition>()
                }));

            var mockLogger = new Mock<ILogger<HomeController>>();


            var secondFeaturesOptions = new Mock<IOptionsMonitor<SecondFeaturesConfiguration>>();
            secondFeaturesOptions.Setup(c=>c.CurrentValue)
                .Returns(new SecondFeaturesConfiguration { EnableSecondWeatherForecast = true });


            var sut = new HomeController(mockWeatherForecaster.Object,
                featuresOptions.Object,
                mockLogger.Object,
                weatherResult.Object,
                secondFeaturesOptions.Object
                );

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("It's sunny right now.", model.WeatherDescription);
        }

        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsRain()
        {

            var mockWeatherForecaster = new Mock<IWeatherForecaster>();
            mockWeatherForecaster.Setup(c => c.GetCurrentWeather()).Returns(new WeatherResult
            {
                WeatherCondition = It.IsAny<WeatherCondition>()
            });

            var featuresOptions = new Mock<IOptionsSnapshot<FeaturesConfiguration>>();
            featuresOptions.SetupGet(c => c.Value)
                .Returns(new FeaturesConfiguration { EnableWeatherForecast = true });

            var secondFeaturesOptions = new Mock<IOptionsMonitor<SecondFeaturesConfiguration>>();
            secondFeaturesOptions.Setup(c => c.CurrentValue)
                .Returns(new SecondFeaturesConfiguration { EnableSecondWeatherForecast = true });

            var weatherResult = new Mock<IDistributedCache<WeatherResult>>();
            weatherResult.Setup(c => c.TryGetValueAsync(It.IsAny<string>()))
                .ReturnsAsync((true, new WeatherResult
                { 
                    WeatherCondition = WeatherCondition.Rain
                }));

            var mockLogger = new Mock<ILogger<HomeController>>();

            var sut = new HomeController(mockWeatherForecaster.Object,
                featuresOptions.Object,
                mockLogger.Object,
                weatherResult.Object,
                secondFeaturesOptions.Object
            );

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result.Result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("We're sorry but it's raining here.", model.WeatherDescription);
        }
    }
}
