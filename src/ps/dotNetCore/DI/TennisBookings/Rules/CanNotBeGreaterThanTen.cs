using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisBookings.Rules
{
    public class CanNotBeGreaterThanTen:INumberRules
    {
        public bool Validate(int input)
        {
            return input < 10;
        }

        public string ErrorMessage => "Value cannot be greater than Ten.";
    }
}
