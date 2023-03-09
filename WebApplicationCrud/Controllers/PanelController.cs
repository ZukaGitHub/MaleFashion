using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Data.FileManager;
using WebApplicationCrud.Models;
using WebApplicationCrud.Models.Identity;
using WebApplicationCrud.ViewModels;
using WebApplicationCrud.ViewModels.HomeVMs;
using WebApplicationCrud.ViewModels.PanelVMs;

namespace WebApplicationCrud.Controllers
{
    public class PanelController : Controller
    {
        public List<SelectListItem> Sizes { get; set; }
        private readonly CRUDdbcontext _ctx;
        private readonly IFileManager _filemanager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public PanelController(CRUDdbcontext ctx, IFileManager fileManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _ctx = ctx;
            _filemanager = fileManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var OwnerId = _userManager.GetUserId(HttpContext.User);
            var MyProducts = _ctx.Products.Where(s => s.OwnerId == OwnerId)
                .Include(s => s.Images)
                .Include(s => s.Tags)
                .Include(g => g.ProductInfos).ThenInclude(s => s.Images).ToList();
            var ViewModel = new PanelIndexViewModel()
            {
                MyProducts = MyProducts
            };




            return View(ViewModel);
        }
        public IActionResult OrderList() {

            var Orders = _ctx.Orders
                .Include(s => s.OrderDetails)
                .Include(s => s.user)
                .ThenInclude(s => s.DeliveryInfo).ToList();

            return View(Orders);

        }


        [HttpGet]
        public IActionResult AddProductPanel(int? productId)

        {

            var Brandss = _ctx.Brands.ToList();
            var Categories = _ctx.Categories.ToList();
            var Sizes = _ctx.TextSizes.ToList();

            var ViewModel = new AddProductVMget()
            {
                Brands = Brandss,
                Categories = Categories,
                Sizes = Sizes,
            };

            if (productId != null)
            {
                ViewModel.EditProductId = (int)productId;

                return View(ViewModel);

            }


            return View(ViewModel);

        }
        [HttpGet]
        public async Task<IActionResult> GetEditProduct(int productId)
        {

            var id = productId;

            var product = new Product();


            product = _ctx.Products.
                Include(s => s.ProductInfos)
                .ThenInclude(s => s.ProductInfoStockAndSizes)
                .Include(s => s.ProductInfos)
                .ThenInclude(s => s.Images)
                .Include(s => s.Tags)
                .SingleOrDefault(p => p.Id == id);



            if (product != null)
            {
                var EditProductVm = new EditProductVm()
                {
                    Id = product.Id,
                    Brand = product.BrandName,
                    Category = product.CategoryName,
                    Description = product.Description,
                    Name = product.Name,
                    Price = product.Price,
                    Tagnames = product.Tags?.Select(s => s.TagName).ToList()


                };


                var productInfos = new List<ProductInfoEditVm>();
                foreach (var productInfo in product.ProductInfos)
                {
                    var productInfoEdit = new ProductInfoEditVm
                    {
                        Color = productInfo.Color,

                        ImageNames = productInfo.Images.Select(s => s.Imagename).ToList(),
                        ThumbnailName = productInfo.ProductInfoThumbnailName,
                        StockAndSize = productInfo.ProductInfoStockAndSizes.Select(s => s).ToList()
                    };
                    productInfos.Add(productInfoEdit);

                }
                EditProductVm.ProductInfos = productInfos;

                var productJson = JsonConvert.SerializeObject(EditProductVm);
                return new JsonResult(productJson);
            }
            return StatusCode(500);

        }

        [HttpGet]
        public async Task<IActionResult> Favourites()
        {
            var user = await _userManager.GetUserAsync(User);

            var productIds = _ctx.FavouriteProducts?.Where(s => s.ApplicationUser == user).Select(s => s.ProductId).ToList();
            var products = new List<Product>();
            if (productIds != null)
            {


                foreach (var id in productIds)
                {
                    if (_ctx.Products.Where(i => i.Id == id)?.SingleOrDefault() == null)
                    {
                        continue;
                    }
                    else
                    {
                        products.Add(_ctx.Products.Where(i => i.Id == id)?.Include(s=>s.ProductInfos).ThenInclude(s=>s.Images).SingleOrDefault());
                    }
                }
                var mappedProduct = MapProducts(products);

                return View(mappedProduct);
            }
            else
            {
                return View(new List<ProductViewModel>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProductPanel(string jsonProducts, ProductImagesVm productImages)
        {

            List<ProductVm> productVms = JsonConvert.DeserializeObject<List<ProductVm>>(jsonProducts);

            var vm = new AddProductVMpost()
            {
                Products = productVms
            };
            var VMproducts = new List<Product>();
            if (vm != null)
            {
                for (int i = 0; i < vm.Products.Count; i++)
                {

                    if (vm.Products[i].SalePercentage == null)
                    {
                        VMproducts[i].SalePercentage = 0;

                    }

                    var newTagNames = vm.Products[i].Tagnames.Split(' ');

                    var productInfos = new List<ProductInfo>();
                    var Tags = new List<Tag>();

                    foreach (var tag in newTagNames)
                    {
                        if (String.IsNullOrWhiteSpace(tag))
                        {
                            continue;
                        }
                        var Tag = new Tag
                        {
                            TagName = tag.ToLower()
                        };
                        Tags.Add(Tag);

                    }
                    //var Brandss = _ctx.Brands.ToList();
                    //ViewBag["Brandss"] = Brandss;

                    int counter = 0;
                    if (vm.Products[i].Id != null) {




                        if (vm.Products[i].Id.HasValue)
                        {
                            var preEditImages = _ctx.Products?.Where(s => s.Id == vm.Products[i].Id).SingleOrDefault().ProductInfos?.SelectMany(s => s.Images.Select(d => d.Imagename)).ToList();

                            if (preEditImages != null && preEditImages.Count > 0)
                            {
                                var postEditImages = productImages.ProductImages[i].RoomImagesVms.SelectMany(s => s.PreviousImages.Select(s => s)).ToList();
                                var Difference = preEditImages.ToList()
                                    .Except(postEditImages).ToList();
                                if (Difference.Count > 0)
                                {
                                    _filemanager.DeleteImages(Difference);
                                    foreach (var image in Difference)
                                    {

                                        _ctx.Images.Remove(_ctx.Images.Where(s => s.Imagename == image).SingleOrDefault());
                                    }
                                }
                            }


                        }
                    }
                    foreach (var Productinfo in vm.Products[i].ProductInfos)
                    {

                        var tempProductInfo = new ProductInfo
                        {
                            Color = Productinfo.Color
                        };

                        var path = "product";
                        var tempListOfImages = new List<Image>();

                        if (Productinfo.ThumbnailEditIndex.HasValue)
                        {
                            tempProductInfo.ProductInfoThumbnailName = Productinfo.ImageNames[(int)Productinfo.ThumbnailEditIndex];
                        }
                        
                        if (productImages != null)
                        {
                            if (productImages.ProductImages[i] != null)
                            {
                               
                                    var imgnames = await _filemanager.SaveImagesAsync(productImages.ProductImages[i].RoomImagesVms[counter].RoomImages, path);

                                if (imgnames != null)
                                {
                                    foreach (var Image in imgnames)
                                    {

                                        var img = new Image()
                                        {
                                            Imagename = Image,
                                        };

                                        tempListOfImages.Add(img);

                                    }
                                    if (productImages.ProductImages[i].RoomImagesVms[counter].PreviousImages != null && 
                                        productImages.ProductImages[i].RoomImagesVms[counter].PreviousImages.Count>0)
                                    {
                                        foreach (var Image in productImages.ProductImages[i].RoomImagesVms[counter].PreviousImages)
                                        {

                                            var img = new Image()
                                            {
                                                Imagename = Image,
                                            };

                                            tempListOfImages.Add(img);

                                        }
                                    }
                                    tempProductInfo.Images = tempListOfImages;
                                    if (productImages.ProductImages[i].RoomImagesVms[counter].ThumbnailIndex != null)
                                    {


                                        tempProductInfo.ProductInfoThumbnailName =
                                            tempListOfImages[(int)productImages.ProductImages[i].RoomImagesVms[counter].ThumbnailIndex].Imagename;
                                    }
                                    if (productImages.ProductImages[i].RoomImagesVms[counter].ThumbnailEditIndex.HasValue)
                                    {


                                        tempProductInfo.ProductInfoThumbnailName =
                                            productImages
                                            .ProductImages[i].
                                            RoomImagesVms[counter].PreviousImages
                                            [(int)productImages.ProductImages[i].RoomImagesVms[counter].ThumbnailEditIndex];
                                    }
                                    counter++;
                                }
                            }
                        }


                        var tempSizesAndStocks = new List<ProductInfoStockAndSize>();
                          
                        foreach (var StockAndSize in Productinfo.Stock)
                        {
                            var tempStockandSize = new ProductInfoStockAndSize()
                            {
                                Stock = StockAndSize.Number,
                                SizeName = StockAndSize.SizeName,

                            };
                            tempSizesAndStocks.Add(tempStockandSize);
                            
                        }
                        tempProductInfo.ProductInfoStockAndSizes = tempSizesAndStocks;
                        productInfos.Add(tempProductInfo);
                    }
                    var salepercentage = int.Parse(vm.Products[i].SalePercentage);
                    var product = new Product()
                    {
                        //Id=(int)vm.Products[i].Id,
                        Name = vm.Products[i].Name,
                        Description = vm.Products[i].Description,
                        BrandName = vm.Products[i].Brand,
                        Price = vm.Products[i].Price,
                        CategoryName = vm.Products[i].Category,
                        ProductInfos = productInfos,
                        TimeAdded=DateTime.Now,
                        SalePercentage = int.Parse(vm.Products[i].SalePercentage),
                        NewPrice = (vm.Products[i].Price * (100 - salepercentage) / 100),
                        Tags = Tags,
                        OwnerId = _userManager.GetUserId(HttpContext.User)

                    };
                    if (vm.Products[i].Id.HasValue)
                    {
                        product.Id = (int)vm.Products[i].Id;
                        _ctx.Products.Remove(_ctx.Products.Where(s => s.Id == product.Id).SingleOrDefault());
                        _ = _ctx.Products.Add(product);
                        await _ctx.SaveChangesAsync();
                        continue;

                    }

                    VMproducts.Add(product);

                }

                _ctx.AddRange(VMproducts);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public async Task<IActionResult> ToggleFavourite(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userFavourites = user.FavouriteProducts ?? new List<FavouriteProduct>();
                if (userFavourites != null && userFavourites.Count() > 0)
                {
                    var userFavouritesIds = userFavourites.Select(s => s.ProductId).ToList();
                    if (userFavouritesIds.Contains(id))
                    {
                        var exFavourite = userFavourites.Where(s => s.ProductId == id).Single();
                        userFavourites.Remove(exFavourite);
                        user.FavouriteProducts = userFavourites;
                        await _ctx.SaveChangesAsync();
                        return View();
                    }
                    if (!userFavouritesIds.Contains(id))
                    {
                        userFavourites.Add(new FavouriteProduct()
                        {
                            userId = user.Id,
                            ProductId = id
                        });
                        user.FavouriteProducts = userFavourites;
                        await _ctx.SaveChangesAsync();
                        return View();
                    }
                }
                userFavourites.Add(new FavouriteProduct()
                {
                    userId = user.Id,
                    ProductId = id
                }) ;
                user.FavouriteProducts = userFavourites;

                await  _ctx.SaveChangesAsync();
                return View();
            }
            return View();
        }


        public IActionResult ChangeOrderState(int[] ids,string state)
        {
            var orders = _ctx.Orders.ToList();

            var ordersToChange = orders.Where(order => ids.Any(id => id == order.Id)).ToList();
            ordersToChange.ForEach(order=> { 
            order.OrderStatus=state;
            });
            _ctx.Orders.UpdateRange(ordersToChange);
            _ctx.SaveChanges();

            return RedirectToAction("OrderList");
        }
        public IActionResult RemoveOrders()
        {

            return RedirectToAction("OrderList");
        } 
        public List<ProductViewModel> MapProducts(List<Product> products)
        {
            var mappedProducts = new List<ProductViewModel>();
            if (products != null && products.Count() > 0)
            {
                foreach (var product in products)
                {
                    var mappedProduct = _mapper.Map<ProductViewModel>(product);
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (userId != null)
                    {
                        var FavouriteProducts = _ctx.FavouriteProducts?.Where(s => s.userId == userId)?.ToList();
                        if (FavouriteProducts != null && FavouriteProducts.Count() > 0)
                        {
                            if (FavouriteProducts.Select(i => i.ProductId).ToList().Contains(product.Id))
                            {
                                mappedProduct.IsUserFavourite = true;
                            }
                        }
                    }
                    var StarRateLinq = _ctx.UserRatings?.Where(s => s.ProductId == product.Id)?.Select(s => s.Rate).ToList();
                    if (StarRateLinq != null && StarRateLinq.Count() > 0)
                    {
                        var StarRate = StarRateLinq?.Average();
                        if (StarRate.HasValue)
                        {
                            mappedProduct.StarRate = (int)Math.Round((double)StarRate);
                        }
                    }




                    if (mappedProduct.ProductInfos != null)
                    {
                        for (int i = 0; i < mappedProduct.ProductInfos.Count(); i++)
                        {
                            mappedProduct.ProductInfos[i].Stock = _mapper.Map<List<StockVm>>(product.ProductInfos[i].ProductInfoStockAndSizes);


                        }


                        mappedProduct.Images = mappedProduct.ProductInfos.SelectMany(s => s.ImageNames.Select(d => d)).ToList();
                    }
                    mappedProducts.Add(mappedProduct);

                }
            }
            return mappedProducts;

        }
        public async Task<IActionResult> Remove(List<int> productIds)
        {
          
            //SSL ginda 
            //daamate produqtebi da blogebi
           
            //documentacia dawere ch
            try
            {


                foreach (var Id in productIds)
                {
                    var currentproduct = _ctx.Products.Include(d => d.Images)
                        .Include(s=>s.ProductInfos).ThenInclude(s => s.Images)
                        .Include(s => s.Comments)
                        
                        .SingleOrDefault(s => s.Id == Id);

                    if (currentproduct != null)
                    {
                        if (currentproduct.ProductInfos != null)
                        {
                            var images = currentproduct.ProductInfos?.SelectMany(s => s.Images).ToList();
                            if (images != null)
                            {
                                _ctx.RemoveRange(images);
                                var imagenames = images.Select(d => d.Imagename).ToList();
                                _filemanager.DeleteImages(imagenames);
                            }
                        }

                        _ctx.Products.Remove(currentproduct);
                        _ctx.Entry(currentproduct).State = EntityState.Deleted;
                        _ctx.SaveChanges();

                    }
                }
            }
            catch(DbUpdateConcurrencyException e)
            {
                var ae = e.Message;
            };
                return RedirectToAction("Index");
            
           
        }
    }
}