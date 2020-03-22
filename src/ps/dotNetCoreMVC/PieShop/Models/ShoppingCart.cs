using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;

namespace PieShop.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;
        public string ShoppingCartId { get; set; }

        public ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            //todo:redis
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            var cartId = session.GetString("CardId") ?? Guid.NewGuid().ToString();
            session.SetString("CardId", cartId);

            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId
            };
        }

        public void AddToCart(Pie pie, int amount)
        {
            var itemsInCart = _appDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
            if (itemsInCart == null)
            {
                itemsInCart = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };
                _appDbContext.ShoppingCartItems.Add(itemsInCart);
            }
            else
            {
                itemsInCart.Amount += amount;
            }

            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var removedItem = _appDbContext.ShoppingCartItems
                .SingleOrDefault(s => s.ShoppingCartId == ShoppingCartId && s.Pie == pie);
            var localAmount = 0;
            if (removedItem != null)
            {
                if (removedItem.Amount > 1)
                {
                    localAmount = --removedItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(removedItem);
                }
            }
            _appDbContext.SaveChanges();
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            var items = _appDbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(c => c.Pie)
                .ToList();
            return items;
        }
        public void ClearCart()
        {
            var items = _appDbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId);
            _appDbContext.ShoppingCartItems.RemoveRange(items);
            _appDbContext.SaveChanges();
        }
        public decimal GetShoppingCartTotal()
        {
            return _appDbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Sum(c => c.Pie.Price * c.Amount);
        }
    }
}
