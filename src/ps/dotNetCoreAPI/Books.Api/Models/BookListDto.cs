using System;

namespace Books.Api.Models
{
    public class BookListDto
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
