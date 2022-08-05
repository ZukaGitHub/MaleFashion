using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Data.FileManager;
using WebApplicationCrud.Models;
using WebApplicationCrud.ViewModels;

namespace WebApplicationCrud.Controllers
{
    public class HomeController : Controller
    {
        private IFileManager _fileManager;
        private CRUDdbcontext _ctx;
        private readonly ShoppingCart _shoppingCart;

        public HomeController(CRUDdbcontext ctx, IFileManager fileManager, ShoppingCart shoppingCart)
        {
            _fileManager = fileManager;
            _ctx = ctx;
            _shoppingCart = shoppingCart;
        }
        public class FormViewModel
        {
            public int id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult ProductPanel(int id)
        {
            var product = _ctx.Products.Include(x => x.Tags).FirstOrDefault(x => x.id == id);


            var Images = _ctx.Images.Where(c => c.productid == product.id).ToList();

            //var RelatedProductIds = _ctx.Tags
            //    .Where
            //    (Tag => Tag.TagName == product.Tags[0].TagName)
            //    .Select(tag => tag.ProductId).ToList().Take(4);
            //var RelatedProducts = new List<Product>();
            //foreach (var Id in RelatedProductIds)
            //{
            //    RelatedProducts.Add(_ctx.Products.SingleOrDefault(prod => prod.id == Id));

            //}
            ////var RelatedProducts = _ctx.Products.Where(prod => prod.Tags.Contains(product.Tags[0])&& prod.Tags.Contains(product.Tags[1])).Take(4).ToList();
            //var RelatedProductList = new List<RelatedProductViewModel>();
            //foreach (var RelatedProduct in RelatedProducts)
            //{
            //    var RelatedProductView = new RelatedProductViewModel()
            //    {
            //        Price = RelatedProduct.price,
            //        ProductId = RelatedProduct.id,
            //        Thumbnails = RelatedProduct.ProductInfos[0].Thumbnails,
            //        ProductInfoId = RelatedProduct.ProductInfos[0].id
            //    };
            //    RelatedProductList.Add(RelatedProductView);

            //}
            List<string> Tags = new List<string>();

            var Temp = product.Tags.ToList();
            foreach (var TagName in Temp)
            {
                Tags.Add(TagName.TagName);
            }

            string AggregateTags = Tags.Aggregate((concat, str) => $"{concat},{str}");

            var ViewValues = new ProductPanelViewModel
            {
                ProductId = product.id,
                price = product.price,
                //RelatedProducts = RelatedProductList,
                category = product.CategoryName,
                Tags = AggregateTags,
                Images = Images,
                name = product.name,
                stock = product.stock,
                desc = product.desc,

            };


            return View(ViewValues);
        }
        public IActionResult blog()
        {
            return View();
        }
        public IActionResult blogdetails()
        {
            return View();
        }
        public IActionResult shop(string InSale, string SortOrder, ShopViewModel ViewModel, int pageNumber = 1)
        {
            int pageSize = 2;
            decimal PageCountCtx = _ctx.Products.Count();
            decimal pageCount = PageCountCtx / pageSize;
            pageCount = Math.Ceiling(pageCount);
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            if (pageNumber > pageCount)
            {
                pageNumber = (int)pageCount;
            }


            var products = _ctx.Products.Include(s => s.Tags).ToList();
            var TagNames = _ctx.Tags.Select(s => s.TagName).ToArray();

            Dictionary<string, int> TopTagNamesDict = 
                TagNames.GroupBy(s => s).ToDictionary(g => g.Key.ToString(), g => g.Count());
            var TopTagNames = TopTagNamesDict.Select(s => s.Key).Take(5).ToList();




            if (!String.IsNullOrEmpty(ViewModel.SearchString))
            {
                products = products.Where(s => s.name.Contains(ViewModel.SearchString)).ToList();
            }
            //Sale-----------------------------------------------------
            if (!String.IsNullOrEmpty(InSale))
            {
                int? ProductCount = products.Count();
                products = products
                    .Where(s => s.SalePercentage > 0)
                    .OrderBy(g => g.name)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var thumbnails = _ctx.Thumbnails.ToList();
                var categories = _ctx.Categories.ToList();
                var brands = _ctx.Brands.ToList();
                var sizes = _ctx.TextSizes.ToList();

                if (!String.IsNullOrEmpty(SortOrder))
                {
                    switch (SortOrder)
                    {
                        case "High To Low":
                            products = products.OrderByDescending(p => p.price).ToList();

                            break;

                        case "Low To High":
                            products = products.OrderBy(p => p.price).ToList();
                            break;


                    }

                }

                var vm = new ShopViewModel()
                {
                    products = products,
                    thumbnails = thumbnails,
                    Categories = categories,
                    Brands = brands,
                    TextSizes = sizes,
                    pageNumber = pageNumber,
                    PageCount = (int)pageCount,
                    PageSize = pageSize,
                    ProductCount = ProductCount,
                    IsSalePage = true,
                    Tags = TopTagNames

                };

                ViewData["Products"] = products;


                ViewData["Thumbnails"] = thumbnails;

                return View(vm);


            }
            //Normal------------------------------------------------------------
            else
            {
                int? ProductCount = products.Count();
                products = products
                    .OrderBy(g => g.name)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var thumbnails = _ctx.Thumbnails.ToList();
                var categories = _ctx.Categories.ToList();
                var brands = _ctx.Brands.ToList();
                var sizes = _ctx.TextSizes.ToList();

                if (!String.IsNullOrEmpty(SortOrder))
                {
                    switch (SortOrder)
                    {
                        case "High To Low":
                            products = products.OrderByDescending(p => p.price).ToList();

                            break;

                        case "Low To High":
                            products = products.OrderBy(p => p.price).ToList();
                            break;


                    }

                }

                var vm = new ShopViewModel()
                {
                    products = products,
                    thumbnails = thumbnails,
                    Categories = categories,
                    Brands = brands,
                    TextSizes = sizes,
                    pageNumber = pageNumber,
                    PageCount = (int)pageCount,
                    PageSize = pageSize,
                    ProductCount = ProductCount,
                    Tags = TopTagNames

                };

                ViewData["Products"] = products;


                ViewData["Thumbnails"] = thumbnails;

                return View(vm);
            }


        }

        public IActionResult checkout()
        {
            return View();
        }
        public IActionResult contacts()
        {
            return View();
        }
        public IActionResult shopdetails()
        {
            return View();
        }

        public IActionResult shoppingcart()
        {
            return View();
        }

        public IActionResult Index()
        {

            var forms = _ctx.Forms.ToList();




            return View(forms);
        }
        [HttpGet("img/product/ProductImages/{Image}")]
        public IActionResult Image(string Image)
        {
            var mime = Image.Substring(Image.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.Imagestream(Image), $"ProductImages/{mime} ");
        }
        [HttpGet("img/product/Thumbnails/{Image}")]
        public IActionResult Thumbnail(string Thumbnail)
        {
            var mime = Thumbnail.Substring(Thumbnail.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.Thumbnailstream(Thumbnail), $"Thumbnails/{mime} ");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FormViewModel vm)
        {
            Form form = new Form
            {
                id = vm.id,
                Name = vm.Name,
                Email = vm.Email,
            };
            _ctx.Add(form);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {
            var forms = _ctx.Forms.ToList();
            if (forms.FirstOrDefault(x => x.id == id) == null)
            {
                return View("bla");
            }
            else
            {

                _ctx.Forms.Remove(_ctx.Forms.FirstOrDefault(x => x.id == id));
            }
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        public async Task<IActionResult> Update(FormViewModel vm)
        {
            Form form = new Form
            {
                id = vm.id,
                Name = vm.Name,
                Email = vm.Email,

            };

            _ctx.Forms.Update(form);



            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Read(int? id)
        {

            var item = _ctx.Forms.FirstOrDefault(x => x.id == id);
            return View(item);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public List<Product> CustomFilter(string CategoryName, string BrandName, int? MaxPrice, string SelectedSize)
        {



            var query = _ctx.Products.AsQueryable();
            if (!String.IsNullOrEmpty(CategoryName))
            {
                query = query.Where(prod => prod.CategoryName == CategoryName);
            }

            if (!String.IsNullOrEmpty(BrandName))
            {
                query = query.Where(prod => prod.BrandName == BrandName);
            }
            // filter by size coming soon



            if (MaxPrice != null)
            {
                query = query.Where(prod => prod.price < MaxPrice && prod.price > MaxPrice - 50);
            }
            return query.ToList();


         
        }

    }
}
