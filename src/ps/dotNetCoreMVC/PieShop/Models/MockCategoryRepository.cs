﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> AllCategories =>
        new List<Category>
        {
            new Category{CategoryId = 1,CategoryName = "Fruit pies",Description = "Fruit pies"},
            new Category{CategoryId = 2,CategoryName = "Cheese cakes",Description = "Cheese cakes"},
            new Category{CategoryId = 3,CategoryName = "Seasonal pies",Description = "Seasonal pies"}
        };
    }
}
