using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.Api.Models;

namespace CourseLibrary.Api.ValidationAttributes
{
    public class CourseTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var course = (CourseManipulationDto)validationContext.ObjectInstance;

            var courseFromValue = value as CourseManipulationDto;

            if (courseFromValue.Title == courseFromValue.Description)
            {
                return new ValidationResult(
                    "The provided description should be different from the title.",
                    new[] { nameof(CourseTitleMustBeDifferentFromDescriptionAttribute) });
            }

            return ValidationResult.Success;
        }
    }
}
