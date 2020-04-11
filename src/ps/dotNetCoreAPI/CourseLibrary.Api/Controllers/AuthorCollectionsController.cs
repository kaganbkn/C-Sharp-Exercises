using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.Api.Entities;
using CourseLibrary.Api.Helpers;
using CourseLibrary.Api.Models;
using CourseLibrary.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.Api.Controllers
{
    [Route("api/authorCollections")]
    [ApiController]
    public class AuthorCollectionsController:ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorCollectionsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
        }

        [HttpGet("({authorIds})",Name = "GetAuthorCollection")]
        public async Task<IActionResult> GetAuthorCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                return BadRequest();
            }
            var authors= await _courseLibraryRepository.GetAuthorsWithIdsAsync(authorIds);
            var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);

            return Ok(authorsDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthorCollection(IEnumerable<AuthorCreationDto> authors)
        {
            if (!authors.Any())
            {
                return NotFound();
            }

            var authorsEntity = _mapper.Map<IEnumerable<Author>>(authors);

            _courseLibraryRepository.AddAuthorRange(authorsEntity);
            await _courseLibraryRepository.SaveAsync();
            
            var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorsEntity);

            var idsToReturn = string.Join(",", authorCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute(
                "GetAuthorCollection",
                new
                {
                    authorIds = idsToReturn
                    //authorIds = authorsEntity.Select(c => c.Id)
                    //authorIds = from author in authorsEntity select author.Id
                },
                authorCollectionToReturn);
        }
    }
}
