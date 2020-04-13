using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.Api.ValidationAttributes;

namespace CourseLibrary.Api.Models
{
    [CourseTitleMustBeDifferentFromDescription]
    public abstract class CourseManipulationDto :IValidatableObject
    {
        // .Net Core firstly looks the DataAnnotations and everything is ok after looks the Validate Method.
        // In other worlds Validate Method doesn't run when DataAnnotations is faulted.
        // Order the validation : Property attribute - Class attribute - Validate Method

        //System.ComponentModel.DataAnnotations.RequiredAttribute
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(1500)]
        public virtual string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == Description)
            {
                yield return new ValidationResult( // immediately return
                    "The provided description should be different from the title.",
                    new[] { "CourseCreationDto" });  // This is optional, related property , usually use the class name 
            }
        }
    }
}
