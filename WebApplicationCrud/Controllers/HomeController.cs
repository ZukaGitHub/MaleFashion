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
            var products = _ctx.Products?
                 .Include(prod => prod.ProductInfos)?.ThenInclude(img => img.Images)?
                 .Include(prod => prod.ProductInfos)?.ThenInclude(stock => stock.ProductInfoStockAndSizes)?
                 .Include(comment => comment.Comments)?
                 .Include(x => x.Tags)?.ToList();


          
            var product=products?.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {

                return RedirectToAction("Shop");

            }
            var relatedProducts = products?.Where(s => s.CategoryName == product.CategoryName).Take(4).ToList();
            var relatedProductsDict = new Dictionary<int, int>();
            var mappedRelatedProducts = new List<ProductViewModel>();
            if (relatedProducts != null && relatedProducts.Count>0)
            {
                foreach (var relatedProduct in relatedProducts)
                {
                    if (relatedProduct.Tags.Intersect(product.Tags)!=null && relatedProduct.Tags.Intersect(product.Tags).Count()>0)
                    {
                        relatedProductsDict.Add(relatedProduct.Id, relatedProduct.Tags.Intersect(product.Tags).Count());
                           
                    }
                }
                
                if (relatedProductsDict != null && relatedProductsDict.Count > 0)
                {
                    relatedProductsDict = relatedProductsDict?.OrderByDescending(s => s.Value).ToDictionary(z => z.Key, d => d.Value);

                    relatedProducts = new List<Product>();

                    for (int i = 0; i < relatedProductsDict.Count(); i++)
                    {
                        relatedProducts.Add(products.FirstOrDefault(prod => prod.Id == relatedProductsDict.ElementAt(i).Key));
                        if (i > 3)
                        {
                            break;
                        }
                    }

                   


                }
                foreach (var prod in relatedProducts)
                {
                    var mappedRelatedProduct = _mapper.Map<ProductViewModel>(prod);

                    if (mappedRelatedProduct.ProductInfos != null)
                    {
                        for (int i = 0; i < mappedRelatedProduct.ProductInfos.Count(); i++)
                        {
                            mappedRelatedProduct.ProductInfos[i].Stock = _mapper.Map<List<StockVm>>(prod.ProductInfos[i].ProductInfoStockAndSizes);


                        }


                        mappedRelatedProduct.Images = mappedRelatedProduct.ProductInfos.SelectMany(s => s.ImageNames.Select(d => d)).ToList();
                    }

                    mappedRelatedProducts.Add(mappedRelatedProduct);

                }
            }

           


          
            
            var mappedProduct = _mapper.Map<ProductViewModel>(product);

            if (mappedProduct.ProductInfos != null)
            {
                for(int i=0; i< mappedProduct.ProductInfos.Count();i++)
                {
                    mappedProduct.ProductInfos[i].Stock = _mapper.Map<List<StockVm>>(product.ProductInfos[i].ProductInfoStockAndSizes);
             
                
                }


                mappedProduct.Images = mappedProduct.ProductInfos.SelectMany(s => s.ImageNames.Select(d=>d)).ToList();
            }

            mappedProduct.RelatedProducts = mappedRelatedProducts;
            mappedProduct.SizeNames = _ctx.Brands?.Select(s => s.Name).ToList();

            
          
          
          


            return View(mappedProduct);
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
                    post.MainComments ??= new List<MainComment>();

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
                    product.Comments ??= new List<MainComment>();

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

            var products = _ctx.Products?
                  .Include(prod => prod.ProductInfos)?.ThenInclude(img => img.Images)?
                  .Include(prod => prod.ProductInfos)?.ThenInclude(stock => stock.ProductInfoStockAndSizes)?
                  .Include(comment => comment.Comments)?
                  .Include(x => x.Tags)?.ToList();
  
            Dictionary<string, int> TopTagNamesDict =
                products?.SelectMany(s => s.Tags?.Select(s => s.TagName))
                .GroupBy(s => s)
                .ToDictionary(g => g.Key.ToString(), g => g.Count());
            var TopTagNames = TopTagNamesDict.Select(s => s.Key).Take(5).ToList();         
           
                int? ProductCount = products.Count();
                products = products
                    .OrderBy(g => g.Name)              
                    .ToList();

            var mappedProducts = new List<ProductViewModel>();
            foreach (var product in products)
            {
                var mappedProduct = _mapper.Map<ProductViewModel>(product);

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


            var sizes = _ctx.TextSizes.ToList();
            var shopViewModel = new ShopViewModel()
                {
                    products = mappedProducts,                   
                    Categories = mappedProducts?.Select(s=>s.CategoryName)?.ToList(),
                    Brands = mappedProducts?.Select(b=>b.BrandName)?.ToList(),
                    TextSizes =sizes?.Select(s=>s.Name)?.ToList(),                              
                    Tags = TopTagNames

                };

          

                return View(shopViewModel);
            


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
     
        public IActionResult Post(int id) =>
            View(_repo.GetPost(id));

      
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
