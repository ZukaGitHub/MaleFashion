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
        private CRUDdbcontext _ctx;
        private IFileManager _filemanager;
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
            var MyProducts = _ctx.Products.Where(s => s.OwnerId == OwnerId).Include(g => g.ProductInfos).ToList();
            var ViewModel = new PanelIndexViewModel()
            {
                MyProducts = MyProducts
            };




            return View(ViewModel);
        }
     

        [HttpGet]
        public IActionResult AddProductPanel(int?[] productIds)

        {
           
            var Brandss = _ctx.Brands.ToList();
            var Categories = _ctx.Categories.ToList();
            var Sizes = _ctx.TextSizes.ToList();

            ViewData["Sizes"] = Sizes;

            ViewData["Brandss"] = Brandss;
            ViewData["Categories"] = Categories;

            if (productIds != null)
            {

            }


            return View();

        }


        [HttpPost]
        public async Task<IActionResult> AddProductPanel(string jsonProducts,ProductImagesVm productImages)
        {

            AddProductVMpost vm = JsonConvert.DeserializeObject<AddProductVMpost>(jsonProducts);

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
                        var Tag = new Tag();
                        Tag.TagName = tag.ToLower();
                        Tags.Add(Tag);

                    }
                    var Brandss = _ctx.Brands.ToList();
                    ViewData["Brandss"] = Brandss;


                    foreach (var Productinfo in vm.Products[i].ProductInfos)
                    {
                        var tempProductInfo = new ProductInfo();
                        tempProductInfo.color = Productinfo.Color;
                       
                        var path = "product";
                        var tempListOfImages = new List<Image>();

                        //var imgnames = await _filemanager.SaveImagesAsync(Productinfo.images, path);
                        //if (imgnames != null)
                        //{
                        //    foreach (var Image in imgnames)
                        //    {

                        //        var img = new Image()
                        //        {
                        //            Imagename = Image,
                        //        };

                        //        tempListOfImages.Add(img);

                        //    }
                        //    tempProductInfo.Images = tempListOfImages;
                        //    tempProductInfo.ProductInfoThumbnailName = tempListOfImages[Productinfo.].Imagename;
                        //}

                        foreach (var StockAndSize in Productinfo.Stock)
                        {
                            var tempStockandSize = new ProductInfoStockAndSize()
                            {
                                stock = StockAndSize.number,
                                SizeName = StockAndSize.sizeName,

                            };
                            var tempSizesAndStocks = new List<ProductInfoStockAndSize>();
                            tempSizesAndStocks.Add(tempStockandSize);
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
        //[HttpGet]
        //public IActionResult EditProductInfo(int ProductInfoId)
        //{
        //    var CurrentProduct = _ctx.ProductInfos.SingleOrDefault(s => s.id == ProductInfoId);
        //    var EditProduct = new EditInfoViewModel
        //    {
        //        id = ProductInfoId,
        //        color = CurrentProduct.color,
        //        Size = CurrentProduct.Size,
        //        Quantity = CurrentProduct.Quantity,
        //        SizeText = CurrentProduct.SizeText
        //    };

        //    return View(EditProduct);
        //}
        //[HttpPost]
        //public IActionResult EditProductInfo(EditInfoViewModel vm)
        //{

        //    var ProductInfo = new ProductInfo
        //    {
        //        color = vm.color,
        //        id = vm.id,
        //        Quantity = vm.Quantity,
        //        Size = vm.Size,
        //        SizeText = vm.SizeText
        //    };


        //    _ctx.ProductInfos.Add(ProductInfo);
        //    _ctx.SaveChanges();





        //    return RedirectToAction("Index");

        //}
        public async Task<IActionResult> Remove(int ProductInfoId, int? IsItProductId)
        {
            if (IsItProductId == ProductInfoId)
            {
                var currentproduct = _ctx.Products.Include(d=>d.Images).SingleOrDefault(s => s.id == ProductInfoId);

                if(currentproduct!=null)
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
                return RedirectToAction("Index");
            }
            else
            {
                var currentProductInfo = _ctx.ProductInfos.Include(d=>d.Images).SingleOrDefault(s => s.id == ProductInfoId);
                    if(currentProductInfo!=null)
                    {
                    if (currentProductInfo.Images != null)
                    {
                        var images = currentProductInfo.Images;
                        _ctx.Images.RemoveRange(images);
                        var imagenames = images.Select(s => s.Imagename).ToList();
                        _filemanager.DeleteImages(imagenames);
                    }
                    _ctx.ProductInfos.Remove(currentProductInfo);
                    await _ctx.SaveChangesAsync();
                     }
               
              
                return RedirectToAction("Index");
            }
        }
    }
}