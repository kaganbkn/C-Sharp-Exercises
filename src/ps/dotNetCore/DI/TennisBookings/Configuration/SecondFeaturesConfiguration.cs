using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TennisBookings.Configuration
{
    public interface ISecondFeaturesConfiguration
    {
        [Required]
        bool EnableSecondWeatherForecast { get; set; }
    }

    public class SecondFeaturesConfiguration : ISecondFeaturesConfiguration
    {
        [Required]
        public bool EnableSecondWeatherForecast { get; set; }
    }
}
