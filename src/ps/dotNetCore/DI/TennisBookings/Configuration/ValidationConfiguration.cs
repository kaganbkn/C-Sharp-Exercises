using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisBookings.Configuration
{
    public class ValidationConfiguration : IValidationConfiguration
    {
        public bool Calculate { get; set; }
    }
}
