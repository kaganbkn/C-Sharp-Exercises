using System.ComponentModel.DataAnnotations;

namespace TennisBookings.Configuration
{
    public class SecondValidationConfiguration
    {
        [MaxLength(5)]
        public string CalculateSecond { get; set; }
    }
}
