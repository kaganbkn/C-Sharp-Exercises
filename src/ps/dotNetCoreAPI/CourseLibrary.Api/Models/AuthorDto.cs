using System;
using System.Collections.Generic;
using CourseLibrary.Api.Entities;

namespace CourseLibrary.Api.Models
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string MainCategory { get; set; }
        public IEnumerable<LinkDto> Links { get; set; }
    }
}
