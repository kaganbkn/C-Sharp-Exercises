using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Books.Api.Entities;
using Books.Api.Filters;
using Books.Api.Models;
using Books.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [Route("api/bookCollections")]
    [ApiController]
    [BooksResultFilter]
    public class BookCollectionsController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookCollectionsController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet("({bookIds})", Name = "GetBookCollection")]
        public async Task<IActionResult> GetBookCollection(IEnumerable<Guid> bookIds)
        //[ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> bookIds)
        {
            var allBooks = await _bookRepository.GetMultipleBooksAsync(bookIds);
            return Ok(allBooks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookCollection(
            [FromBody] IEnumerable<BookCreationDto> bookCollection) // [FromBody] --> request body - [FromUri] --> query string
        {
            //todo:Author id is really exist?
            var newCollection = _mapper.Map<IEnumerable<Book>>(bookCollection);
            _bookRepository.AddMultipleBook(newCollection);

            await _bookRepository.SaveChangesAsync();

            var allBooks = await _bookRepository.GetMultipleBooksAsync(
                newCollection.Select(c => c.Id).ToList());

            var allBookIds = string.Join(",", allBooks.Select(c => c.Id));

            return CreatedAtRoute("GetBookCollection", new { bookIds = allBookIds }, allBooks);
        }
    }
}
