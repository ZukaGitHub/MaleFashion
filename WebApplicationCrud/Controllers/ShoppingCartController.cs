using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Data.FileManager;
using WebApplicationCrud.Models;
using WebApplicationCrud.ViewModels;

namespace WebApplicationCrud.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IFileManager _fileManager;
        private CRUDdbcontext _ctx;
        private readonly ShoppingCart _shoppingCart;
        private readonly string _sessionId;

        public ShoppingCartController(CRUDdbcontext ctx, IFileManager fileManager, ShoppingCart shoppingCart)
        {
            _fileManager = fileManager;
            _ctx = ctx;
            _shoppingCart = shoppingCart;
            _sessionId = HttpContext.Session.Id;
          
        }
        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            int ShoppingCartItemsAmount = _shoppingCart.ShoppingCartItems.Count();
            

            var vm = new ShoppingCartViewModel
            {
                ShoppingCartItemsAmount = ShoppingCartItemsAmount,
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(vm);


        }
        public IActionResult CantAddToCart()
        {
            return View();
        }
        public RedirectToActionResult AddToShoppingCart(int productid, int productInfoId,string size,int amount)
        {
            var Product = _ctx.Products.Include(s => s.ProductInfos).SingleOrDefault(p => p.Id == productid);
            var selectedProduct = Product.ProductInfos.FirstOrDefault(p => p.ProductId == productid && p.Id == productInfoId);
            var selectedProductInfoStockAndSize = selectedProduct.ProductInfoStockAndSizes.SingleOrDefault(s => s.SizeName == size);




            _ctx.StockOnHolds.Add(new Models.ShoppingCartModels.StockOnHold()
            {
                ProductInfoStockAndSize = selectedProductInfoStockAndSize,
                Amount = amount,
                ExpiryDate = DateTime.Now.AddMinutes(20),
                SessionId = _sessionId
            }) ;
            
            if(amount<1 || amount> selectedProductInfoStockAndSize.Stock)
            {
                return RedirectToAction("CantAddToCart");
            }
            if (selectedProduct != null)
            {
                selectedProductInfoStockAndSize.Stock= selectedProductInfoStockAndSize.Stock - amount;
                _shoppingCart.AddToCart(selectedProduct, amount, Product,size);
                _ctx.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public RedirectToActionResult RemoveFromShoppingCart(int productId, int productInfoId)
        {
            var selectedProduct = _ctx.ProductInfos
                .FirstOrDefault(p => p.ProductId == productId && p.Id == productInfoId);
            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }
            return RedirectToAction("Index");
        }
    }
}