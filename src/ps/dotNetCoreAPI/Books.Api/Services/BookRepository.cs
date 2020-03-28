using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Api.Contexts;
using Books.Api.Entities;
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

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
