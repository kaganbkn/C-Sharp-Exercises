using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdeToFood.Data
{
    public class RestaurantData : IRestaurantData
    {
        private readonly List<Restaurant> restaurants;
        public RestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant(){Id = 1,Name = "Pizza",Location = "Sarıyer"},
                new Restaurant(){Id = 2,Name = "Hamburger",Location = "Kağıthane"}
            };
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return from item in restaurants
                   orderby item.Name descending
                   select item;
        }
    }
}
