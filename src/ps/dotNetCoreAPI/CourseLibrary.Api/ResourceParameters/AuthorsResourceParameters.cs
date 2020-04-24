using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.Api.ResourceParameters
{
    public class AuthorsResourceParameters
    {
        private const int MaxPageSize = 200;
        public string SearchQuery { get; set; }
        public string MainCategory { get; set; }
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 100;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string OrderBy { get; set; } = "Name";
    }
}
