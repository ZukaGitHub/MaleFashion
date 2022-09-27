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
using System.Threading.Tasks;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Data.FileManager;
using WebApplicationCrud.Models;
using WebApplicationCrud.ViewModels;
using WebApplicationCrud.ViewModels.PanelVMs;

namespace WebApplicationCrud.Controllers
{
    public class PanelController : Controller
    {
        public List<SelectListItem> Sizes { get; set; }
        private readonly CRUDdbcontext _ctx;
        private readonly IFileManager _filemanager;
        private readonly UserManager<IdentityUser> _userManager;

        public PanelController(CRUDdbcontext ctx, IFileManager fileManager, UserManager<IdentityUser> userManager)
        {
            _ctx = ctx;
            _filemanager = fileManager;
            _userManager = userManager;
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
                Include(s=>s.ProductInfos)
                .ThenInclude(s=>s.ProductInfoStockAndSizes)
                .Include(s=>s.ProductInfos)
                .ThenInclude(s=>s.Images)                             
                .SingleOrDefault(p => p.Id == id);



            if (product != null)
            {
                var EditProductVm = new EditProductVm()
                {
                    Id = product.Id,
                    Brand = product.BrandName,
                    Category = product.CategoryName,
                    Description = product.desc,
                    Name = product.name,
                    Price = product.price,
                    Tagnames = product.Tags?.Select(s => s.TagName).ToList()
                   

                };
               
               
                var productInfos = new List<ProductInfoEditVm>();
                foreach(var productInfo in product.ProductInfos)
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


        [HttpPost]
        public async Task<IActionResult> AddProductPanel(string jsonProducts,ProductImagesVm productImages)
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
                        var Tag = new Tag
                        {
                            TagName = tag.ToLower()
                        };
                        Tags.Add(Tag);

                    }
                    var Brandss = _ctx.Brands.ToList();
                    ViewBag["Brandss"] = Brandss;

                    int counter = 0;
                    foreach (var Productinfo in vm.Products[i].ProductInfos)
                    {

                        var tempProductInfo = new ProductInfo
                        {
                            Color = Productinfo.Color
                        };

                        var path = "product";
                        var tempListOfImages = new List<Image>();

                        if (Productinfo.ImageNames != null && vm.Products[i].Id!=null)
                        {
                            var preEditProduct = _ctx.Products?.Where(s => s.Id == vm.Products[i].Id).SingleOrDefault();
                            if (preEditProduct != null)
                            {
                                var preEditImages = preEditProduct.Images;
                                var Difference = preEditImages.Select(s => s.Imagename).ToList()
                                    .Except(Productinfo.ImageNames).ToList();
                                if (Difference.Count > 0)
                                {
                                    _filemanager.DeleteImages(Difference);
                                    foreach (var image in Difference)
                                    {
                                        _ctx.Remove(_ctx.Images.Where(s => s.Imagename == image));
                                    }
                                }
                               
                                if (Productinfo.ThumbnailEditIndex.HasValue)
                                {
                                    tempProductInfo.ProductInfoThumbnailName = Productinfo.ImageNames[(int)Productinfo.ThumbnailEditIndex];
                                }

                            }
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
                                    tempProductInfo.Images = tempListOfImages;
                                    if (productImages.ProductImages[i].RoomImagesVms[counter].ThumbnailIndex!=null)
                                    {


                                        tempProductInfo.ProductInfoThumbnailName =
                                            tempListOfImages[(int)productImages.ProductImages[i].RoomImagesVms[counter].ThumbnailIndex].Imagename;
                                    }
                                    counter++;
                                }
                            }
                        }
                       

                        foreach (var StockAndSize in Productinfo.Stock)
                        {
                            var tempStockandSize = new ProductInfoStockAndSize()
                            {
                                Stock = StockAndSize.Number,
                                SizeName = StockAndSize.SizeName,

                            };
                            var tempSizesAndStocks = new List<ProductInfoStockAndSize>
                            {
                                tempStockandSize
                            };
                            tempProductInfo.ProductInfoStockAndSizes=tempSizesAndStocks;
                        }
                        productInfos.Add(tempProductInfo);
                    }
                    var salepercentage = int.Parse(vm.Products[i].SalePercentage);
                    var product = new Product()
                    {
                        name = vm.Products[i].Name,
                        desc = vm.Products[i].Description,
                        BrandName = vm.Products[i].Brand,
                        price = vm.Products[i].Price,
                        CategoryName = vm.Products[i].Category,
                        ProductInfos = productInfos,
                        SalePercentage = int.Parse(vm.Products[i].SalePercentage),
                        NewPrice = (vm.Products[i].Price * (100 - salepercentage) / 100),
                        Tags = Tags,
                        OwnerId = _userManager.GetUserId(HttpContext.User)
                    
                    };
                    if (vm.Products[i].Id.HasValue)
                    {
                        product.Id = (int)vm.Products[i].Id;

                        _ctx.Update(product);
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
        
        public async Task<IActionResult> Remove(List<int> productIds)
        {

            foreach (var Id in productIds)
            {
                var currentproduct = _ctx.Products.Include(d => d.Images).SingleOrDefault(s => s.Id == Id);

                if (currentproduct != null)
                {
                    if (currentproduct.ProductInfos != null)
                    {
                        var images = currentproduct.ProductInfos.SelectMany(s => s.Images).ToList();
                        if (images != null)
                        {
                            _ctx.RemoveRange(images);
                            var imagenames = images.Select(d => d.Imagename).ToList();
                            _filemanager.DeleteImages(imagenames);
                        }
                    }
                    _ctx.Products.Remove(currentproduct);
                    await _ctx.SaveChangesAsync();

                }
            }
                return RedirectToAction("Index");
            
           
        }
    }
}