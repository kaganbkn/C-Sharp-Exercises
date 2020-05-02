using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisBookings.Rules
{
    public interface INumberRules
    {
        bool Validate(int input);
        string ErrorMessage { get;}
    }
}
