using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Books.Api.Contexts;
using Books.Api.Entities;
using Books.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Api.Services
{
    public class BookRepository : IBookRepository, IDisposable
    {
        private readonly BooksContext _context;
        public BookRepository(BooksContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books.Include(c => c.Author).ToListAsync();
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            return await _context.Books.Include(c => c.Author)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void AddBook(Book bookToAdd)
        {
            if (bookToAdd == null)
            {
                throw new ArgumentNullException(nameof(bookToAdd));
            }

            _context.Add(bookToAdd);
        }

        public void AddMultipleBook(IEnumerable<Book> books)
        {
            if (books == null)
            {
                throw new ArgumentNullException(nameof(books));
            }

            _context.AddRange(books);
        }

        public async Task<IEnumerable<Book>> GetMultipleBooksAsync(IEnumerable<Guid> bookIds)
        {
            return await _context.Books.Where(c => bookIds.Contains(c.Id))
                 .Include(c => c.Author).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            //return true if one or more entities wew changed.
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
