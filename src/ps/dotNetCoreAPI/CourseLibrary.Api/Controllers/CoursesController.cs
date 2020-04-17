using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.Api.Entities;
using CourseLibrary.Api.Models;
using CourseLibrary.Api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            // If the environment is development client got an stack trace with (500)error 
            // on the other side if the environment is production we got only 500 error, when we throw an exception.
            // We can override empty 500 error in Startup class in production.

            // throw new Exception("An error occured.");

            var courses = await _courseLibraryRepository.GetCoursesAsync(authorId);
            if (!courses.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpGet("{courseId}", Name = "GetCourseForAuthor")]
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
                new { authorId, courseId = courseToReturn.Id },
                courseToReturn);
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourseForAuthor(Guid courseId, Guid authorId, CourseForUpdateDto courseForUpdate)
        {
            if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
            {
                return NotFound();
            }

            var course = await _courseLibraryRepository.GetCourseAsync(authorId, courseId);
            if (course == null)
            {
                //return NotFound();

                // After Upserting --> İf doesn't exist, create.

                var createEntity = _mapper.Map<Course>(courseForUpdate);
                createEntity.Id = courseId;

                _courseLibraryRepository.AddCourse(authorId, createEntity);
                await _courseLibraryRepository.SaveAsync();

                return CreatedAtRoute("GetCourseForAuthor",
                    new { authorId, courseId },
                    _mapper.Map<CourseDto>(createEntity));
            }

            _mapper.Map(courseForUpdate, course);  // We directly map that two object without any definition in AutoMapperProfile.cs

            //  We don't need to Update function because we already update the entity with mapper.
            // _courseLibraryRepository.UpdateCourse(course);

            await _courseLibraryRepository.SaveAsync();

            var courseToReturn = _mapper.Map<CourseDto>(course);

            // Usually returned NoContent() or Ok() in put request.
            return CreatedAtRoute("GetCourseForAuthor",
                new { authorId, courseId },
                courseToReturn);
        }

        // Content-Type header : application/json-patch+json
        // JSON Patch Operations : Add,Remove,Replace,Copy,Move,Test
        // Added new type in Startup.cs

        [HttpPatch("{courseId}")]
        public async Task<IActionResult> PartiallyUpdateCourseForAuthor(Guid authorId,
            Guid courseId,
            JsonPatchDocument<CourseForUpdateDto> patchDocument)
        {
            if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
            {
                return NotFound();
            }

            var courseFromRepo = await _courseLibraryRepository.GetCourseAsync(authorId, courseId);
            if (courseFromRepo == null)
            {
                // Upserting

                var createCourse = new CourseForUpdateDto();

                patchDocument.ApplyTo(createCourse, ModelState);
                
                if (!TryValidateModel(createCourse))
                {
                    return ValidationProblem(ModelState);
                }

                var courseToCreateEntity=_mapper.Map<Course>(createCourse);
                courseToCreateEntity.Id = courseId;

                _courseLibraryRepository.AddCourse(authorId, courseToCreateEntity);
                await _courseLibraryRepository.SaveAsync();

                return CreatedAtRoute("GetCourseForAuthor",
                    new { authorId, courseId },
                    _mapper.Map<CourseDto>(courseToCreateEntity));

            }

            var courseToPatch = _mapper.Map<CourseForUpdateDto>(courseFromRepo);
            patchDocument.ApplyTo(courseToPatch, ModelState); //This model state parameter provide us avoid input text error.

            if (!TryValidateModel(courseToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(courseToPatch, courseFromRepo);

            // We don't need the update method because mapping process has already provided us that.
            //_courseLibraryRepository.UpdateCourse(courseFromRepo);
            await _courseLibraryRepository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
            {
                return NotFound();
            }

            var course = await _courseLibraryRepository.GetCourseAsync(authorId, courseId); 
            if (course == null)
            {
                return NotFound();
            }
            _courseLibraryRepository.DeleteCourse(course);
            await _courseLibraryRepository.SaveAsync();

            return NoContent();
        }

        public override ActionResult ValidationProblem( // Any validation error has occured, we return 422 error with this overriding code otherwise we return 400.
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}