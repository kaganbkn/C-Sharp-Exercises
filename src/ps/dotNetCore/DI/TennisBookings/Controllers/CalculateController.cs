using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TennisBookings.Configuration;
using TennisBookings.Models;
using TennisBookings.Rules;

namespace TennisBookings.Controllers
{
    public class CalculateController : Controller
    {
        private readonly IEnumerable<INumberRules> _numberRules;
        private readonly SecondValidationConfiguration _secondValidation;

        public CalculateController(IEnumerable<INumberRules> numberRules,
            IOptions<SecondValidationConfiguration> secondValidation)
        {
            _numberRules = numberRules;
            _secondValidation = secondValidation.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Action Injection --> I.E : If our services are used only one action ...
        [HttpPost]
        public IActionResult Index(CalculateViewModel input, [FromServices]IValidationConfiguration validationConfiguration)  // Action Injection 
        {

            var validationErrors = new List<string>();

            if (validationConfiguration.Calculate && _secondValidation.CalculateSecond == "true")
            {
                foreach (var rule in _numberRules)
                {
                    if (!rule.Validate(input.Number))
                    {
                        validationErrors.Add(rule.ErrorMessage);
                    }

                }

            }

            if (!validationErrors.Any())
                validationErrors.Add("Value is valid.");

            var returnToValidationErrors = string.Join("\n", validationErrors);

            ModelState.AddModelError("", returnToValidationErrors);
            return View();
        }
    }
}
