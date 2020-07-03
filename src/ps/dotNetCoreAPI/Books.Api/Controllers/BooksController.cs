using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Books.Api.Entities;
using Books.Api.ExternalModels;
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
        private readonly IServiceProvider _serviceProvider;

        public BooksController(IBookRepository bookRepository, IMapper mapper, IServiceProvider serviceProvider)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        [BooksResultFilter]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.GetBooksAsync();
        }

        [Route("{id}", Name = "GetBook")]
        [HttpGet]
        //[BookResultFilter]
        [BookWithCoversResultFilterAttribute]
        public async Task<IActionResult> GetBook(Guid id) // We don't use the Async signature in method name in controller based on convention.
        {

            // IServiceProvider
            //var book=(IBookRepository)_serviceProvider.GetService(typeof(IBookRepository));
            //var bookEntity1 = await book.GetBookAsync(id);

            var bookEntity = await _bookRepository.GetBookAsync(id);
            //var newBookEntity = _mapper.Map<BookListDto>(bookEntity);
            if (bookEntity == null)
            {
                return NotFound();
            }
            //return Ok(bookEntity);


            ///// EXTERNAL API CALL
            //var bookCover = await _bookRepository.GetBookCoverAsync("dummy");

            var bookCovers = await _bookRepository.GetBookCoversAsync(id);
            // var propertyBag = new Tuple<Book, IEnumerable<BookCover>>(bookEntity, bookCovers); // We access the value via item1 and item2.
            // (Book book, IEnumerable<BookCover> bookCovers) propertyBag1 = (bookEntity, bookCovers);// We access the value via book and bookCovers.

            // return Ok((book: bookEntity, bookCover: bookCovers)); // Another way to define a tuple. 
            return Ok((bookEntity, bookCovers));
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
