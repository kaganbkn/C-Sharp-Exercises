﻿using System;

namespace CourseLibrary.Api.Models
{
    public class CreateAuthorDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string MainCategory { get; set; }
    }
}