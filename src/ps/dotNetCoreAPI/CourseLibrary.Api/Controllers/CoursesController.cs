using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.Api.Entities;
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

        [HttpGet("{courseId}",Name = "GetCourseForAuthor")]
        public async Task<IActionResult> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            var courses = await _courseLibraryRepository.GetCourseAsync(authorId, courseId);
            if (courses == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(courses));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourseForAuthor(Guid authorId, CourseCreationDto course)
        {
            if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
            {
                return NotFound();
            }

            var courseEntity = _mapper.Map<Course>(course);
            _courseLibraryRepository.AddCourse(authorId, courseEntity);
            await _courseLibraryRepository.SaveAsync();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);

            return CreatedAtRoute("GetCourseForAuthor", 
                new { authorId, courseId = courseToReturn.Id},
                courseToReturn);
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourseForAuthor(Guid courseId, Guid authorId,CourseForUpdateDto courseForUpdate)
        {
            var course = await _courseLibraryRepository.GetCourseAsync(authorId, courseId);
            if (course == null)
            {
                return NotFound();
            }

            _mapper.Map(courseForUpdate, course);  // We directly map that two object without any definition in AutoMapperProfile.cs

            //  We don't need to Update function because we already update the entity with mapper.
            // _courseLibraryRepository.UpdateCourse(course);

            await _courseLibraryRepository.SaveAsync();

            var courseToReturn = _mapper.Map<CourseDto>(course);
            
            // Usually returned NoContent() or Ok() in put request.
            return CreatedAtRoute("GetCourseForAuthor",
                new { authorId, courseId},
                courseToReturn);
        }
    }
}