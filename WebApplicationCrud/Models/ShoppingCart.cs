using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplicationCrud.Data.DbContext;

namespace WebApplicationCrud.Models
{
    public class ShoppingCart
    {
        private readonly CRUDdbcontext _ctx;

        public ShoppingCart(CRUDdbcontext ctx)
        {
            _ctx = ctx;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }


        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = services.GetService<CRUDdbcontext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };

        }

        public void AddToCart(ProductInfo product, int amount, Product Product)
        {
            var shoppingCartItem = _ctx.shoppingCartItems
                .SingleOrDefault(s => s.ProductInfo.Id == product.Id
                && s.ShoppingCartId == ShoppingCartId
                && s.ProductInfo.ProductId == product.ProductId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    Product = Product,
                    ShoppingCartId = ShoppingCartId,
                    ProductInfo = product,
                    Amount = 1
                };
                _ctx.shoppingCartItems.Add(shoppingCartItem);
            }



            else
            {
                shoppingCartItem.Amount++;
            }
            _ctx.SaveChanges();
        }


        public int RemoveFromCart(ProductInfo product)
        {
            var shoppingCartItem = _ctx.shoppingCartItems
                .SingleOrDefault(s => s.ProductInfo.Id == product.Id
                && s.ShoppingCartId == ShoppingCartId
                && s.ProductInfo.ProductId == product.ProductId);
            var localAmount = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _ctx.shoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _ctx.SaveChanges();
            return localAmount;

        }


        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            var ShoppingCartItems =
                _ctx.shoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(p => p.Product)
                .Include(p => p.ProductInfo)
                .ToList();


            return ShoppingCartItems;

        }


        public void ClearCart()
        {
            var cartItems = _ctx.shoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _ctx.shoppingCartItems.RemoveRange(cartItems);
            _ctx.SaveChanges();

        }

        public float GetShoppingCartTotal()
        {
            var total = _ctx.shoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;

        }


    }
}
