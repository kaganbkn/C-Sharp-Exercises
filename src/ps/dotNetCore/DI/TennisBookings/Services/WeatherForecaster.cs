﻿using System;

namespace TennisBookings.Services
{
    public class WeatherForecaster : IWeatherForecaster
    {
        public WeatherResult GetCurrentWeather()
        {
            // Pretend we call out to a remote 3rd party API here to get the real forecast!
            // For demo purposes, the result is hard-coded.

            return new WeatherResult
            {
                WeatherCondition = WeatherCondition.Sun
            };
        }

        public int GetCurrentDegree()
        {
            var rand = new Random();
            return rand.Next(0, 30);
        }
    }
}
