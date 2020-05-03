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
        private readonly IValidationConfiguration _validationConfiguration;

        public CalculateController(IEnumerable<INumberRules> numberRules, IValidationConfiguration validationConfiguration)
        {
            _numberRules = numberRules;
            _validationConfiguration = validationConfiguration;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(CalculateViewModel input)
        {

            var validationErrors=new List<string>();

            if (_validationConfiguration.Calculate)
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
