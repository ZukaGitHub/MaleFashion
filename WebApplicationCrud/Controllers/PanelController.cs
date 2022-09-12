using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Data.FileManager;
using WebApplicationCrud.Models;
using WebApplicationCrud.ViewModels;

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
        public IActionResult AddProductPanel()

        {
            var Brandss = _ctx.Brands.ToList();
            var Categories = _ctx.Categories.ToList();

            ViewData["Brandss"] = Brandss;
            ViewData["Categories"] = Categories;
            var TextSizes = _ctx.TextSizes.ToList();
            var temp = new List<SelectListItem>();
            var temp2 = new List<string>();
            foreach (var Item in TextSizes)
            {
                var SelectedItem = new SelectListItem()
                {
                    Value = Item.id.ToString(),
                    Text = Item.name

                };
                string selecteditem2 = Item.name;
                temp2.Add(selecteditem2);

                temp.Add(SelectedItem);
            }
            this.Sizes = temp;


            return View(new AddProductVMget()
            {
                Sizes = Sizes,
                Sizes2 = temp2
            });

        }


        [HttpPost]
        public async Task<IActionResult> AddProductPanel(AddProductVMpost vm)
        {
            var VMproducts = new List<Product>();
            if (vm != null)
            {
                for (int i = 0; i < vm.productVms.Count; i++)
                {

                    if (vm.productVms[i].salePercentage == null)
                    {
                        VMproducts[i].SalePercentage = 0;

                    }
                    var newTagNames = vm.productVms[i].tagnames.Split(' ');

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


                    foreach (var Productinfo in vm.productVms[i].productInfoVms)
                    {
                        var tempProductInfo = new ProductInfo();
                        tempProductInfo.color = Productinfo.color;
                       
                        var path = "product";
                        var tempListOfImages = new List<Image>();
                        int counter = 0;
                        foreach (var Image in Productinfo.images)
                        {
                            var imgname = await _filemanager.SaveImageAsync(Image,path);
                            var img = new Image()
                            {
                                Imagename = imgname,
                            };
                          
                            tempListOfImages.Add(img);
                            counter++;
                        }
                        tempProductInfo.Images = tempListOfImages;
                        tempProductInfo.ProductInfoThumbnailName = tempListOfImages[Productinfo.Thumbnail].Imagename;

                        foreach (var StockAndSize in Productinfo.stockVms)
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
                    var salepercentage = int.Parse(vm.productVms[i].salePercentage);
                    var product = new Product()
                    {
                        name = vm.productVms[i].name,
                        desc = vm.productVms[i].description,
                        BrandName = vm.productVms[i].brand,
                        price = vm.productVms[i].price,
                        CategoryName = vm.productVms[i].category,
                        ProductInfos = productInfos,
                        SalePercentage = int.Parse(vm.productVms[i].salePercentage),
                        NewPrice = (vm.productVms[i].price * (100 - salepercentage) / 100),
                        Tags = Tags,
                        OwnerId = _userManager.GetUserId(HttpContext.User)
                    
                    };

                    VMproducts.Add(product);
                    
                }

                _ctx.AddRange(VMproducts);
                _ctx.SaveChanges();
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

        public IActionResult EditProduct(int ProductId)
        {
            var currentproduct = _ctx.Products.SingleOrDefault(s => s.id == ProductId);


            //Coming Soon-------------------------------------------------



            return View();
        }
        public async Task<IActionResult> Remove(int ProductInfoId, int? IsItProductId)
        {
            if (IsItProductId == ProductInfoId)
            {
                var currentproduct = _ctx.Products.SingleOrDefault(s => s.id == ProductInfoId);
                _ctx.Products.Remove(currentproduct);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var CurrentProduct = _ctx.ProductInfos.SingleOrDefault(s => s.id == ProductInfoId);
                _ctx.ProductInfos.Remove(CurrentProduct);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }
}