using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Api.Entities;
using Books.Api.Models;

namespace Books.Api.Services
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(Guid id);
        void AddBook(Book book);
        void AddMultipleBook(IEnumerable<Book> books);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Book>> GetMultipleBooksAsync(IEnumerable<Guid> bookIds);
    }
}
