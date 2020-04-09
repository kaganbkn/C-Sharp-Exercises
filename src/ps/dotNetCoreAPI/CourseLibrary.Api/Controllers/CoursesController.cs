using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.Api.Models;
using CourseLibrary.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesForAuthor(Guid authorId)
        {
            // Handling Faults 
            // If the environment is development client got an stack trace - (500)error on the other side
            // if the environment is production we got only 500 error, when we throw an exception.
            // We can override empty 500 error in Startup class in production.
            // throw new Exception("An error occured.");

            var courses = await _courseLibraryRepository.GetCoursesAsync(authorId);
            if (!courses.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseForAuthor(Guid authorId,Guid courseId)
        {
            var courses = await _courseLibraryRepository.GetCourseAsync(authorId, courseId);
            if (courses == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(courses));
        }


    }
}