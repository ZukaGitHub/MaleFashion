using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public ShoppingCartController(CRUDdbcontext ctx, IFileManager fileManager, ShoppingCart shoppingCart)
        {
            _fileManager = fileManager;
            _ctx = ctx;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            int ShoppingCartItemsAmount = 0;
            foreach (var item in items)
            {
                ShoppingCartItemsAmount += item.Amount;
            }

            var vm = new ShoppingCartViewModel
            {
                ShoppingCartItemsAmount = ShoppingCartItemsAmount,
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(vm);


        }
        public RedirectToActionResult AddToShoppingCart(int productid, int productInfoId)
        {
            var Product = _ctx.Products.Include(s => s.ProductInfos).SingleOrDefault(p => p.Id == productid);
            var selectedProduct = Product.ProductInfos.FirstOrDefault(p => p.ProductId == productid && p.Id == productInfoId);
            //var selectedProduct = _ctx.ProductInfos
            //    .FirstOrDefault(p => p.ProductId == productid && p.id==productInfoId);
            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, 1, Product);
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