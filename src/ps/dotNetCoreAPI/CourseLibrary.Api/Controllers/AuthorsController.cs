using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.Api.Entities;
using CourseLibrary.Api.Helpers;
using CourseLibrary.Api.Models;
using CourseLibrary.Api.ResourceParameters;
using CourseLibrary.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

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

        [HttpGet(Name = "GetAuthors")]
        public IActionResult GetAuthors([FromQuery] AuthorsResourceParameters authorsResourceParameters)
        // we must add [FromQuery(Name = "category")] here because its a complex type otherwise we don't need it.
        {
            var authors = _courseLibraryRepository.GetAuthors(authorsResourceParameters);

            var previousPageLink = authors.HasPrevious
                ? CreateAuthorsResourceUri(authorsResourceParameters, ResourceUriType.PreviousPage)
                : null;

            var nextPageLink = authors.HasNext
                ? CreateAuthorsResourceUri(authorsResourceParameters, ResourceUriType.NextPage)
                : null;

            var paginationMetaData = new
            {
                totalCount = authors.TotalCount,
                pageSize = authors.PageSize,
                currentPage = authors.CurrentPage,
                totalPage = authors.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonConvert.SerializeObject(paginationMetaData));

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

            authorsDto.Links = CreateLinksForAuthor(authorId);

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

        [HttpDelete("{authorId}",Name = "DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(Guid authorId)
        {
            var authorEntity = await _courseLibraryRepository.GetAuthorAsync(authorId);
            if (authorEntity == null)
            {
                return NotFound();
            }

            var courses = await _courseLibraryRepository.GetCoursesAsync(authorId); // We delete all author's courses.
            courses.ToList().ForEach(c =>
            {
                _courseLibraryRepository.DeleteCourse(c);
            });

            _courseLibraryRepository.DeleteAuthor(authorEntity);
            await _courseLibraryRepository.SaveAsync();
            return NoContent();
        }

        private string CreateAuthorsResourceUri(
           AuthorsResourceParameters authorsResourceParameters,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetAuthors",
                      new
                      {
                          pageNumber = authorsResourceParameters.PageNumber - 1,
                          pageSize = authorsResourceParameters.PageSize,
                          mainCategory = authorsResourceParameters.MainCategory,
                          searchQuery = authorsResourceParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetAuthors",
                      new
                      {
                          pageNumber = authorsResourceParameters.PageNumber + 1,
                          pageSize = authorsResourceParameters.PageSize,
                          mainCategory = authorsResourceParameters.MainCategory,
                          searchQuery = authorsResourceParameters.SearchQuery
                      });
                default:
                    return Url.Link("GetAuthors",
                    new
                    {
                        pageNumber = authorsResourceParameters.PageNumber,
                        pageSize = authorsResourceParameters.PageSize,
                        mainCategory = authorsResourceParameters.MainCategory,
                        searchQuery = authorsResourceParameters.SearchQuery
                    });
            }

        }
        private IEnumerable<LinkDto> CreateLinksForAuthor(Guid authorId)  //HATEOAS
        {
            var links = new List<LinkDto>();

            links.Add(
              new LinkDto(Url.Link("GetAuthor", new { authorId }),
              "self",
              "GET"));

            links.Add(
               new LinkDto(Url.Link("DeleteAuthor", new { authorId }),
               "delete_author",
               "DELETE"));

            links.Add(
                new LinkDto(Url.Link("CreateCourseForAuthor", new { authorId }),
                "create_course_for_author",
                "POST"));

            links.Add(
               new LinkDto(Url.Link("GetCoursesForAuthor", new { authorId }),
               "courses",
               "GET"));

            return links;
        }
    }
}
