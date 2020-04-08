using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.Api.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var now = DateTime.UtcNow;
            var age = now.Year - dateTimeOffset.Year;
            if (now < dateTimeOffset.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
}
