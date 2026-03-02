using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techora.Models;

namespace Bl
{
    public interface ICart
    {
        List<Cart> GetCartByCustomer(int customerId);
        bool AddToCart(int customerId, int productId, int quantity);
        bool RemoveFromCart(int cartId);
        bool ClearCart(int customerId);
    }
    public class ClsCart : ICart
    {
        private readonly DevicesContext context;

        public ClsCart(DevicesContext ctx)
        {
            context = ctx;
        }

        public List<Cart> GetCartByCustomer(int customerId)
        {
            return context.Carts
                .Include(c => c.Product)
                .ThenInclude(p => p.ProductImages)
                .Where(c => c.CustomerId == customerId)
                .ToList();
        }

        public bool AddToCart(int customerId, int productId, int quantity)
        {
            try
            {
                var existing = context.Carts
                    .FirstOrDefault(c => c.CustomerId == customerId && c.ProductId == productId);

                if (existing != null)
                {
                    existing.Quantity += quantity;
                    context.Entry(existing).State = EntityState.Modified;
                }
                else
                {
                    var cart = new Cart
                    {
                        CustomerId = customerId,
                        ProductId = productId,
                        Quantity = quantity
                    };
                    context.Carts.Add(cart);
                }

                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveFromCart(int cartId)
        {
            try
            {
                var item = context.Carts.Find(cartId);
                if (item != null)
                {
                    context.Carts.Remove(item);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ClearCart(int customerId)
        {
            try
            {
                var items = context.Carts.Where(c => c.CustomerId == customerId).ToList();
                context.Carts.RemoveRange(items);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}