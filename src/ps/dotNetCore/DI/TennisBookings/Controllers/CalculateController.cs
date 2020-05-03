using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TennisBookings.Configuration;
using TennisBookings.Models;
using TennisBookings.Rules;

namespace TennisBookings.Controllers
{
    public class CalculateController : Controller
    {
        private readonly IEnumerable<INumberRules> _numberRules;

        public CalculateController(IEnumerable<INumberRules> numberRules)
        {
            _numberRules = numberRules;
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

            if (validationConfiguration.Calculate)
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
