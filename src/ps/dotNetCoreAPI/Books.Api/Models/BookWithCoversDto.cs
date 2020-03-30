using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Api.Entities;
using Books.Api.ExternalModels;

namespace Books.Api.Models
{
    public class BookWithCoversDto :BookListDto
    {
        public IEnumerable<BookCover> BookCovers { get; set; }
    }
}
