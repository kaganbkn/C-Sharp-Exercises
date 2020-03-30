using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Books.Api.Contexts;
using Books.Api.Entities;
using Books.Api.ExternalModels;
using Books.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace Books.Api.Services
{
    public class BookRepository : IBookRepository, IDisposable
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly BooksContext _context;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly ILogger _logger;

        public BookRepository(BooksContext context, IHttpClientFactory httpClientFactory, ILogger<BookRepository> logger)
        {
            _httpClientFactory = httpClientFactory;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
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

        public async Task<BookCover> GetBookCoverAsync(string coverId)
        {
            var httpClient = _httpClientFactory.CreateClient();   // Repository is used only for a db operations.
            var response = await httpClient.GetAsync(
                $"https://localhost:44320/api/bookcovers/{coverId}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BookCover>(
                    await response.Content.ReadAsStringAsync());
            }

            return null;
        }
        public async Task<IEnumerable<BookCover>> GetBookCoversAsync(Guid bookId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var bookCovers = new List<BookCover>();
            var bookCoversUrls = new[]
            {
                $"https://localhost:44320/api/bookcovers/{bookId}-dummycover1",
                $"https://localhost:44320/api/bookcovers/{bookId}-dummycover2?returnFault=true",
                $"https://localhost:44320/api/bookcovers/{bookId}-dummycover3",
                $"https://localhost:44320/api/bookcovers/{bookId}-dummycover4",
                $"https://localhost:44320/api/bookcovers/{bookId}-dummycover5",
            };

            _cancellationTokenSource = new CancellationTokenSource();

            //create the tasks - task list
            var downloadBookCoverTasksQuery = from bookCoversUrl in bookCoversUrls // from...in...select
                                              select DownloadBookCoverAsync(httpClient, bookCoversUrl, _cancellationTokenSource.Token);

            //start the tasks
            var downloadBookCoverTasks = downloadBookCoverTasksQuery.ToList(); // Execution deferred to ToList() command.

            try
            {
                return await Task.WhenAll(downloadBookCoverTasks);
            }
            catch (OperationCanceledException operationCanceledException)
            {
                _logger.LogInformation($"{operationCanceledException.Message}"); // Logging to VS output console.
                foreach (var item in downloadBookCoverTasks)
                {
                    _logger.LogInformation($"Task {item.Id} has status {item.Status}");
                }
                return new List<BookCover>();
            }


            //foreach (var item in bookCoversUrls)
            //{
            //    var response = await httpClient.GetAsync(item);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        bookCovers.Add(JsonConvert.DeserializeObject<BookCover>(
            //            await response.Content.ReadAsStringAsync()));
            //    }
            //}
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

        public async Task<BookCover> DownloadBookCoverAsync(HttpClient httpClient, string item, CancellationToken cancellationToken)
        {

            var response = await httpClient.GetAsync(item, cancellationToken); // We pass the cancellationToken parameter for when one task is cancel, cancel all tasks.

            if (response.IsSuccessStatusCode) // If response is null.
            {
                var result = JsonConvert.DeserializeObject<BookCover>(
                    await response.Content.ReadAsStringAsync());
                return result;
            }

            _cancellationTokenSource.Cancel();

            return null;
        }
    }
}
