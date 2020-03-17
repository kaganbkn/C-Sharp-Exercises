using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class MockPieRepository : IPieRepository
    {
        private readonly ICategoryRepository _categoryRepository=new MockCategoryRepository();
        public IEnumerable<Pie> AllPies => new List<Pie>
        {
            new Pie() { PieId = 1,Name = "Strawberry Pie",Price=15.95M,ShortDescription = "Strawberry Pie"},
            new Pie() { PieId = 2,Name = "Cheese cake",Price=15.95M,ShortDescription = "Cheese cake"},
            new Pie() { PieId = 3,Name = "Rhubarb Pie",Price=15.95M,ShortDescription = "Rhubarb Pie"},
            new Pie() { PieId = 4,Name = "Pumpkin Pie",Price=15.95M,ShortDescription = "Pumpkin Pie",}
        };
        public IEnumerable<Pie> PiesOfTheWeek { get; }
        public Pie GetPieById(int pieId)
        {
            return AllPies.FirstOrDefault(c => c.PieId == pieId);
        }
    }
}
