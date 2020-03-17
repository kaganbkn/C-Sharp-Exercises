using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> AllCategories { get;}
    }
}
