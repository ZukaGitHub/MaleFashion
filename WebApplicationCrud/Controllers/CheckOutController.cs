using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Data.FileManager;
using WebApplicationCrud.Models;
using WebApplicationCrud.Models.Identity;
using WebApplicationCrud.ViewModels;
using WebApplicationCrud.ViewModels.CheckOutVMs;

namespace WebApplicationCrud.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IFileManager _fileManager;
        private readonly CRUDdbcontext _ctx;
        private readonly ShoppingCart _shoppingCart;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CheckOutController(CRUDdbcontext ctx, IFileManager fileManager, ShoppingCart shoppingCart,UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            _fileManager = fileManager;
            _ctx = ctx;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user =  await _userManager.GetUserAsync(HttpContext.User);

                var items = _shoppingCart.GetShoppingCartItems();
                _shoppingCart.ShoppingCartItems = items;
                if (user != null )
                {
                    if (_shoppingCart.ShoppingCartItems?.Count > 0)
                    {
                        var shoppingCartViewModel = new ShoppingCartViewModel()
                        {
                            ShoppingCart = _shoppingCart,
                            ShoppingCartItemsAmount = _shoppingCart.ShoppingCartItems.Count,
                            ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()


                        };
                        var checkOutViewModel = new CheckOutViewModel()
                        {
                            ShoppingCartViewModel = shoppingCartViewModel,
                            DeliveryInfo = user.DeliveryInfo ?? new DeliveryInfo()
                        };
                        return View(checkOutViewModel);
                    }
                    else
                    {
                        return RedirectToAction("Shop","Home");
                    }
                }
                else
                {
                    return RedirectToAction("Login","Auth");
                }
            
               

            }
            else
            {
                return  RedirectToAction("Login", "Auth");
            }
          
        }
        public async Task<IActionResult> EditAddress(DeliveryInfoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var mappedDeliveryInfo = _mapper.Map<DeliveryInfo>(vm);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                user.DeliveryInfo = mappedDeliveryInfo;
                await _ctx.SaveChangesAsync();

                return View();
            }
            return RedirectToAction("Index");

        }
        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source=stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Description = "Sample Charge",
                Currency = "usd",
                Customer = customer.Id
            });

            return View();
        }
        public async Task<IActionResult> PlaceOrder()
        {



          
            var ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
            float orderTotal = _shoppingCart.GetShoppingCartTotal();
            var Order = new Order();
            var OrderDetails = new List<OrderDetails>(ShoppingCartItems.Count);
            var OrderDetail = new OrderDetails();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var productInfos = new List<ProductInfo>();


            var sessionId = HttpContext.Session.Id;
            var stockOnHold = _ctx.StockOnHolds.Where(s => s.SessionId == sessionId).ToList();
            _ctx.RemoveRange(stockOnHold);
            foreach (var Item in ShoppingCartItems)
            {

                try
                {
                    var productInfo = _ctx.ProductInfos
                   .Where(prod => prod.ProductInfoStockAndSizes.Select(size => size.SizeName).Contains(Item.SizeText)
                   && prod.Id == Item.ProductInfo.Id).SingleOrDefault();
                    var productInfoStockAndSize = productInfo.ProductInfoStockAndSizes.SingleOrDefault(s => s.SizeName == Item.SizeText);
                       productInfoStockAndSize.Stock = productInfoStockAndSize.Stock - Item.Amount;
                    if(productInfoStockAndSize.Stock < 0)
                    {
                        continue;
                    }
                    else
                    {
                        productInfos.Add(productInfo);
                    }

                }
                catch(NullReferenceException e)
                {
                    continue;
                }

                _ctx.UpdateRange(productInfos);

                OrderDetail.SizeText = Item.SizeText;
                OrderDetail.Quantity = Item.Amount;              
                OrderDetails.Add(OrderDetail);
            }

            Order.SessionId = sessionId;
            Order.OrderDetails = OrderDetails;
            Order.OrderDate = DateTime.Now;
            Order.OrderTotal = orderTotal;
            Order.user = user;
            Order.OrderStatus = "Received";
            _ctx.Orders.Add(Order);    
            await _ctx.SaveChangesAsync();
            _shoppingCart.ClearCart();
            return RedirectToAction("OrderPlacedSuccessfully", "Home");
        }
    }
}