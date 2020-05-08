using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace TennisBookings.Configuration
{
    public class PersonConfigurationValidation : IValidateOptions<PersonConfiguration>
    {
        public ValidateOptionsResult Validate(string name, PersonConfiguration options)
        {
            // We can use name parameter for each subsection.
            if (!options.IsEnable)
                return ValidateOptionsResult.Success;
            if (string.IsNullOrEmpty(options.Name))
                return ValidateOptionsResult.Fail("Name is required.");
            if (options.IsMature && options.Age < 20)
                return ValidateOptionsResult.Fail("Age can't be less than 20.");
            return ValidateOptionsResult.Success;
        }
    }
}
