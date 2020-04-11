using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.Api.Entities;
using CourseLibrary.Api.Models;
using CourseLibrary.Api.ResourceParameters;
using CourseLibrary.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.Api.Controllers
{
    [Route("api/authors")]
    [ApiController]  //automatically return 400 bad request,bad input vs.. 
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetAuthors([FromQuery] AuthorsResourceParameters authorsResourceParameters)
        // we must add [FromQuery(Name = "category")] here because its a complex type otherwise we don't need it.
        {
            var authors = await _courseLibraryRepository.GetAuthorsAsync(authorsResourceParameters);
            var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(authorsDto);
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        public async Task<IActionResult> GetAuthor(Guid authorId)
        {
            var author = await _courseLibraryRepository.GetAuthorAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }
            var authorsDto = _mapper.Map<AuthorDto>(author);

            return Ok(authorsDto); // 200 Ok
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthor(AuthorCreationDto authorCreation)
        {
            //if (author == null) // This control provide us from ApiController attribute.
            //{
            //    return BadRequest();
            //}

            var authorEntity = _mapper.Map<Author>(authorCreation);
            _courseLibraryRepository.AddAuthor(authorEntity);
            await _courseLibraryRepository.SaveAsync();

            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor",
                new { authorId = authorToReturn.Id },
                authorToReturn);
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS");
            return Ok();
        }
    }
}
