using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisBookings.Rules
{
    public class CanNotBeLessThanZero : INumberRules
    {
        public bool Validate(int input)
        {
            return input > 0;
        }

        public string ErrorMessage => "Value cannot be less than 0.";
    }
}
