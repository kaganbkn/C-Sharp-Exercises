using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly AppDbContext _appDbContext;

        public OrderRepository(ShoppingCart shoppingCart, AppDbContext appDbContext)
        {
            _shoppingCart = shoppingCart;
            _appDbContext = appDbContext;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _appDbContext.Orders.Add(order);

            _appDbContext.SaveChanges();
            var itemsInList = _shoppingCart.GetShoppingCartItems();
            foreach (var item in itemsInList)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    Price = item.Pie.Price,
                    PieId = item.Pie.PieId,
                    OrderId = order.OrderId
                };
                _appDbContext.OrderDetails.Add(orderDetail);
            }

            _appDbContext.SaveChanges();
        }
    }
}
