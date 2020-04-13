using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.Api.ValidationAttributes;

namespace CourseLibrary.Api.Models
{
    public class CourseForUpdateDto :CourseManipulationDto
    {
        [Required]
        public override string Description { get; set; }
    }
}
