using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Data.FileManager;
using WebApplicationCrud.Models;

namespace WebApplicationCrud.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IFileManager _fileManager;
        private readonly CRUDdbcontext _ctx;
        private readonly ShoppingCart _shoppingCart;

        public CheckOutController(CRUDdbcontext ctx, IFileManager fileManager, ShoppingCart shoppingCart)
        {
            _fileManager = fileManager;
            _ctx = ctx;
            _shoppingCart = shoppingCart;
        }

        public IActionResult PlaceOrder()
        {
            var ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
            float orderTotal = _shoppingCart.GetShoppingCartTotal();
            var Order = new Order();
            var OrderDetails = new List<OrderDetails>(ShoppingCartItems.Count);
            var OrderDetail = new OrderDetails();


            foreach (var Item in ShoppingCartItems)
            {


                OrderDetail.ProductId = Item.Product.Id;
                OrderDetail.Quantity = Item.Amount;
                OrderDetail.Name = Item.Product.Name;
                //OrderDetail.Size = Item.ProductInfo.Size;
                //OrderDetail.SizeText = Item.ProductInfo.SizeText;
                OrderDetail.Color = Item.ProductInfo.Color;
                OrderDetails.Add(OrderDetail);


            }
            Order.OrderDetails = OrderDetails;
            Order.OrderDate = DateTime.Now;
            Order.OrderTotal = orderTotal;
            _ctx.Orders.Add(Order);
            _ctx.SaveChanges();

            return RedirectToAction("index", "Home");
        }
    }
}