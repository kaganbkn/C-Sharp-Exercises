using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Books.Api.Entities;
using Books.Api.Filters;
using Books.Api.Models;
using Books.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [BooksResultFilter]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.GetBooksAsync();
        }

        [Route("{id}", Name = "GetBook")]
        [HttpGet]
        [BookResultFilter]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var bookEntity = await _bookRepository.GetBookAsync(id);
            //var newBookEntity = _mapper.Map<BookListDto>(bookEntity);
            if (bookEntity == null)
            {
                return NotFound();
            }
            return Ok(bookEntity);
        }

        [HttpPost]
        [BookResultFilter]
        public async Task<IActionResult> CreateBook(BookCreationDto bookToCreate)
        {
            var newBook = _mapper.Map<Book>(bookToCreate);

            _bookRepository.AddBook(newBook);
            await _bookRepository.SaveChangesAsync();

            // Fetch(refetch) the book from the data store, including the author.
            await _bookRepository.GetBookAsync(newBook.Id);

            return CreatedAtRoute("GetBook", new { id = newBook.Id }, newBook);
        }


    }
}
