using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Data.FileManager;
using WebApplicationCrud.Data.Repository;
using WebApplicationCrud.Models;
using WebApplicationCrud.Models.BlogModels;
using WebApplicationCrud.ViewModels;
using WebApplicationCrud.ViewModels.BlogVMs;
using WebApplicationCrud.ViewModels.HomeVMs;

namespace WebApplicationCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileManager _fileManager;
        private readonly CRUDdbcontext _ctx;
        private readonly ShoppingCart _shoppingCart;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public HomeController(
            CRUDdbcontext ctx,
            IFileManager fileManager,
            ShoppingCart shoppingCart,
             IRepository repo,
             IMapper mapper
            )
        {
            _repo = repo;
            _fileManager = fileManager;
            _ctx = ctx;
            _shoppingCart = shoppingCart;
            _mapper = mapper;
        }
       
        public IActionResult About()
        {
            return View();
        }
        public IActionResult ProductPanel(int id)
        { 
            var product = _ctx.Products?
                 .Include(prod=>prod.ProductInfos)?.ThenInclude(img=>img.Images)?
                 .Include(prod => prod.ProductInfos)?.ThenInclude(stock=>stock.ProductInfoStockAndSizes)?
                 .Include(comment=>comment.Comments)?
                 .Include(x => x.Tags)?.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                
            }
            
            List<string> Tags = product.Tags?.Select(s => s.TagName).ToList();
            var ViewValues = _mapper.Map<ProductViewModel>(product);

            //var productInfos = new List<ProductInfoViewModel>();
            //if (product.ProductInfos != null)
            //{
            //    foreach (var Info in product.ProductInfos)
            //    {
            //        var prodInfoVm = new ProductInfoViewModel()
            //        {
            //            Color = Info.Color,
            //            ImageNames = Info.Images?.Select(s => s.Imagename).ToList(),
            //            ProductInfoThumbnailName = Info.ProductInfoThumbnailName,
            //            Stock = Info.ProductInfoStockAndSizes?.Select(s => new StockVm()
            //            {
            //                SizeName = s.SizeName,
            //                Number=s.Stock
            //            }).ToList()
            //        };
            //        productInfos.Add(prodInfoVm);
            //    }
            //}

            //List<ProductViewModel> relatedProducts = new List<ProductViewModel>();
            //if (Tags != null)
            //{
            //    foreach(var tag in Tags)
            //    {

            //    }
            //}

            //var ViewValues = new ProductViewModel
            //{
            //    Id = product.Id,
            //    Price = product.Price,            
            //    CategoryName = product.CategoryName,
            //    Comments = product.Comments?.ToList(),
            //    Tags = Tags,          
            //    Name = product.Name,
            //    BrandName=product.BrandName,        
            //    Description = product.Description,
            //    ProductInfos=productInfos

            //};


            return View(ViewValues);
        }
        public IActionResult Blog(int pageNumber, string category, string search)
        {
            if (pageNumber < 1)
                return RedirectToAction("Blog", new { pageNumber = 1, category });

            var vm = _repo.GetAllPosts(pageNumber, category, search);

            return View(vm);
        }
        public IActionResult Blogdetails(int id)
        {
            var post = _repo.GetPost(id);



            int CommentCount = 0;
            if (post.MainComments != null && post.MainComments.Count > 0)
            {
                CommentCount += post.MainComments.Count;

                foreach (var maincomments in post.MainComments)
                {
                    if (maincomments.SubComments != null && maincomments.SubComments.Count > 0)
                    {
                        CommentCount += maincomments.SubComments.Count;
                    }


                }


            }
            var postVm = new BlogDetailsViewModel()
            {
                Author = post.Author,
                Body = post.Body,
                Category = post.Category,
                CreationDate = post.Created,
                Description = post.Description,
                ImageName = post.Image,
                Quote = post.Qoute,
                Id = post.Id,
                Comments = post.MainComments?.ToList(),
                QuoteAuthor = post.QouteAuthor,
                Tags = post.Tags,
                Title = post.Title,
                CommentCount = CommentCount
            };
            return View(postVm);
        }
        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
                if (vm.IsPost)
                {
                    return RedirectToAction("BlogDetails", new { id = vm.Id });
                }
            if (vm.IsProduct)
            {
                return RedirectToAction("ProductPanel", new { id = vm.Id });
            }



            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity.Name;


            if (vm.IsPost)
            {
                var post = _repo.GetPost(vm.Id);
                if (vm.MainCommentId == 0)
                {
                    post.MainComments = post.MainComments ?? new List<MainComment>();

                    post.MainComments.Add(new MainComment
                    {
                        Author = userName,
                        AuthorId = userId,
                        Message = vm.Message,
                        Created = DateTime.Now,
                    });

                    _repo.UpdatePost(post);
                }
                else
                {
                    var comment = new SubComment
                    {
                        MainCommentId = vm.MainCommentId,
                        Message = vm.Message,
                        Author = userName,
                        Created = DateTime.Now,
                    };
                    _repo.AddSubComment(comment);
                }

                await _repo.SaveChangesAsync();

                return RedirectToAction("BlogDetails", new { id = vm.Id });
            }
            if (vm.IsProduct)
            {
                var product = _repo.GetProduct(vm.Id);
                if (vm.MainCommentId == 0)
                {
                    product.Comments = product.Comments ?? new List<MainComment>();

                    product.Comments.Add(new MainComment
                    {
                        Author = userName,
                        AuthorId = userId,
                        Message = vm.Message,
                        Created = DateTime.Now,
                    });

                    _repo.UpdateProduct(product);
                }
                else
                {
                    var comment = new SubComment
                    {
                        MainCommentId = vm.MainCommentId,
                        Message = vm.Message,
                        Author = userName,
                        Created = DateTime.Now,
                    };
                    _repo.AddSubComment(comment);
                }

                await _repo.SaveChangesAsync();

                return RedirectToAction("ProductPanel", new { id = vm.Id });

            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public IActionResult Shop(ShopViewModel ViewModel)
        {
         
           
           


            var products = _ctx.Products.Include(s => s.Tags)
                .Include(s=>s.ProductInfos)
                .Include(d=>d.Comments).ToList();
            var TagNames = _ctx.Tags.Select(s => s.TagName).ToArray();

            Dictionary<string, int> TopTagNamesDict =
                TagNames.GroupBy(s => s).ToDictionary(g => g.Key.ToString(), g => g.Count());
            var TopTagNames = TopTagNamesDict.Select(s => s.Key).Take(5).ToList();




            if (!String.IsNullOrEmpty(ViewModel.SearchString))
            {
                products = products.Where(s => s.Name.Contains(ViewModel.SearchString)).ToList();
            }
           
                int? ProductCount = products.Count();
                products = products
                    .OrderBy(g => g.Name)
                    
                
                    .ToList();

                var thumbnails = _ctx.Thumbnails.ToList();
                var categories = _ctx.Categories.ToList();
                var brands = _ctx.Brands.ToList();
                var sizes = _ctx.TextSizes.ToList();

              

                var vm = new ShopViewModel()
                {
                    products = products,
                    thumbnails = thumbnails,
                    Categories = categories,
                    Brands = brands,
                    TextSizes = sizes,                
                  
                    Tags = TopTagNames

                };

                ViewData["Products"] = products;


                ViewData["Thumbnails"] = thumbnails;

                return View(vm);
            


        }

        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Shoppingcart()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
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




        public IActionResult Post(int id) =>
            View(_repo.GetPost(id));

        //[HttpGet("/Image/{image}")]
        //[ResponseCache(CacheProfileName = "Monthly")]
        //public IActionResult Image(string image) =>
        //     new FileStreamResult(
        //         _fileManager.ImageStream(image),
        //         $"image/{image.Substring(image.LastIndexOf('.') + 1)}");

        [HttpGet]
        public IActionResult CustomFilter(string CategoryName, string BrandName, int? MaxPrice, string SelectedSize)
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
                query = query.Where(prod => prod.Price < MaxPrice && prod.Price > MaxPrice - 50);
            }
            return View(query.ToList());


         
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
