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
using WebApplicationCrud.Models.AdministrationModels;
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
                 .Include(comment => comment.Comments)?.ThenInclude(subcomment=>subcomment.SubComments)?
                 .Include(x => x.Tags)?.ToList();


            var stockOnHold = _ctx.StockOnHolds.Where(x => x.ExpiryDate < DateTime.Now).ToList();
            if (stockOnHold.Count > 0)
            {
                var stockToReturn = _ctx.ProductInfoStockAndSize.Where(prodinfoss => stockOnHold.Any(soh => soh.ProductInfoStockAndSizeId == prodinfoss.Id));
                foreach(var stock in stockToReturn)
                {
                    stock.Stock = stock.Stock + stockOnHold.FirstOrDefault(soh => soh.ProductInfoStockAndSizeId == stock.Id).Amount;
                }
                _ctx.StockOnHolds.RemoveRange(stockOnHold);
                _ctx.SaveChanges();
            }


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
                mappedRelatedProducts = MapProducts(relatedProducts);
                //foreach (var prod in relatedProducts)
                //{
                //    var mappedRelatedProduct = _mapper.Map<ProductViewModel>(prod);

                //    if (mappedRelatedProduct.ProductInfos != null)
                //    {
                //        for (int i = 0; i < mappedRelatedProduct.ProductInfos.Count(); i++)
                //        {
                //            mappedRelatedProduct.ProductInfos[i].Stock = _mapper.Map<List<StockVm>>(prod.ProductInfos[i].ProductInfoStockAndSizes);


                //        }


                //        mappedRelatedProduct.Images = mappedRelatedProduct.ProductInfos.SelectMany(s => s.ImageNames.Select(d => d)).ToList();
                //    }

                //    mappedRelatedProducts.Add(mappedRelatedProduct);

                //}
            }

           


          
            
            var mappedProduct = _mapper.Map<ProductViewModel>(product);
            var StarRateLinq = _ctx.UserRatings?.Where(s => s.ProductId == product.Id)?.Select(s => s.Rate).ToList();
            if (StarRateLinq != null && StarRateLinq.Count > 0)
            {
                var StarRate = StarRateLinq?.Average();
                if (StarRate.HasValue)
                {
                    mappedProduct.StarRate = (int)Math.Round((double)StarRate);
                }
            }

            if (mappedProduct.ProductInfos != null)
            {
                for(int i=0; i< mappedProduct.ProductInfos.Count();i++)
                {
                    mappedProduct.ProductInfos[i].Stock = _mapper.Map<List<StockVm>>(product.ProductInfos[i].ProductInfoStockAndSizes);
             
                
                }


                mappedProduct.Images = mappedProduct.ProductInfos.SelectMany(s => s.ImageNames.Select(d=>d)).ToList();
            }

            mappedProduct.RelatedProducts = mappedRelatedProducts;
            mappedProduct.SizeNames = _ctx.TextSizes?.Select(s => s.Name).ToList();

            
          
          
          


            return View(mappedProduct);
        }
        public IActionResult Blog(int pageNumber, string category, string search)
        {
            var validatedPageNumber = _repo.PageCountValidity(pageNumber);

            
            var vm = _repo.GetAllPosts(validatedPageNumber, category, search);

            return View(vm);
        }
        public IActionResult OrderPlacedSuccessfully()
        {
            return View();
        }
        public IActionResult BlogDetails(int id)
        {
            var post = _repo.GetPost(id);
            var relatedPosts = _repo.GetPrevAndNextPosts(id);

           
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
                RelatedBlogs=relatedPosts,
                Comments = post.MainComments?.ToList(),
                QuoteAuthor = post.QouteAuthor,
                Tags = post.Tags,
                Title = post.Title,
                CommentCount = CommentCount
            };
            return View(postVm);
        }

        [HttpPost]
        public IActionResult NewLetter(string email)
        {
            if (!String.IsNullOrWhiteSpace(email))
            {
                var newsletter = new NewsLetter()
                {
                    Email = email,
                    DateReceived=DateTime.Now
                };
                _ctx.NewsLetters.Add(newsletter);
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
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

            var mappedProducts = MapProducts(products);
           

            var sizes = _ctx.TextSizes.ToList();
            var shopViewModel = new ShopViewModel()
                {
                    products = mappedProducts,                   
                    Categories = mappedProducts?.Select(s=>s.CategoryName)?.ToList(),
                    Brands = mappedProducts?.Select(b=>b.BrandName)?.ToList(),
                    TextSizes =sizes?.Select(s=>s.Name)?.ToList(),                              
                    Tags = TopTagNames,
                    MinPrice=mappedProducts.OrderBy(s=>s.Price).Select(s=>s.Price).FirstOrDefault(),
                    MaxPrice = mappedProducts.OrderByDescending(s => s.Price).Select(s => s.Price).FirstOrDefault(),


            };

          

                return View(shopViewModel);
            


        }
       
        [HttpPost]
        public async Task<IActionResult> StarRating(int? productId,int? rate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (productId.HasValue && rate.HasValue && userId != null)
            {
                var prevUserRating = _ctx.UserRatings?.Where(s => s.ProductId == productId && s.UserId == userId)?.SingleOrDefault();
                if (prevUserRating != null)
                {
                    prevUserRating.Rate = (int)rate;
                    _ctx.UserRatings.Update(prevUserRating);
                   await _ctx.SaveChangesAsync();
                    return View();
                }
                if (_ctx.Products.Where(s => s.Id == (int)productId) != null)
                {
                    var userRating = new UserRating()
                    {
                        UserId = userId,
                        ProductId = (int)productId,
                        Rate = (int)rate
                    };
                    _ctx.UserRatings.Add(userRating);
                    await _ctx.SaveChangesAsync();
                    return View();
                }
            }

            //test this functionality-----------------------------------------------------
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }

     

        public IActionResult Index()
        {


            var products = (_ctx.Products?.Include(s=>s.Images).Include(s=>s.ProductInfos)
                .ThenInclude(d=>d.Images).ToList()) ?? new List<Product>();
            if(products!=null && products.Count > 0)
            {
                var hotSale = MapProduct(products.OrderByDescending(s => s.SalePercentage)?.First());
            
                var hotSlaes = MapProducts(products.OrderByDescending(s => s.SalePercentage)?.Take(8).ToList());
                var newArrivals=MapProducts(products.OrderByDescending(s=>s.TimeAdded)?.Take(8).ToList());
                var bestSellers=MapProducts(products.OrderByDescending(s=>s.TimesSold)?.Take(8).ToList());
                
                var indexProducts = new HomeIndexViewModel()
                {
                   BestSellers=bestSellers.Count()>0 ? bestSellers : null,
                   HotSale= hotSale ?? null,  
                   NewArrivals=newArrivals.Count()>0 ? newArrivals :null,
                   HotSales=hotSlaes.Count()>0 ? hotSlaes :null,

                   


                };
                return View(indexProducts);
            }

            return View(new HomeIndexViewModel());
           

            
        }
     
        public IActionResult Post(int id) =>
            View(_repo.GetPost(id));

      
        [HttpPost]
        public IActionResult CustomFilter(CustomFiltersVM vm)
        {

            

            var query = _ctx.Products.Include(s => s.ProductInfos).ThenInclude(d => d.Images).Include(t=>t.Tags).AsQueryable();
            if (vm != null)
            {
                
                if (vm.CategoryNames != null && vm.CategoryNames.Count() > 0)
                {

                    query = query.Where(prod => vm.CategoryNames.Any(cat => cat == prod.CategoryName));

                }

                if (vm.BrandNames != null && vm.BrandNames.Count() > 0)
                {
                    query = query.Where(prod => vm.BrandNames.Any(brandName => brandName == prod.BrandName));
                }

                if (vm.MaxPrice.HasValue)
                {
                    query = query.Where(prod => prod.Price <= (int)vm.MaxPrice);
                }
                if (vm.MinPrice.HasValue)
                {
                    query = query.Where(prod => prod.Price >= (int)vm.MinPrice);
                }
                if (vm.SelectedSizes != null && vm.SelectedSizes.Count() > 0)
                {
                   
                    query = query.Where
                          (prod => prod.ProductInfos.SelectMany
                          (s => s.ProductInfoStockAndSizes)
                          .Select(s => s.SizeName).Any(size => vm.SelectedSizes.Contains(size)));
                    
                }
                if (vm.SelectedTags != null && vm.SelectedTags.Count() > 0)
                {
                    query = query.Where(s => s.Tags.Select(t=>t.TagName).Any(tag=>vm.SelectedTags.Contains(tag)));
                }
                if (!String.IsNullOrEmpty(vm.FilterSort))
                {
                    if (vm.FilterSort =="High To Low")
                    {
                        query = query.OrderByDescending(s => s.Price);
                    }
                    if (vm.FilterSort == "Low To High")
                    {
                        query = query.OrderBy(s => s.Price);
                    }
                }
                var mappedFilteredProducts = MapProducts(query.ToList());
             
                return PartialView("_ProductListing", mappedFilteredProducts);
            }

            var mappedProducts =MapProducts( query.ToList());
            return PartialView("_ProductListing", mappedProducts);




        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
        public ProductViewModel MapProduct(Product product)
        {

            var mappedProduct = new ProductViewModel();
            if (product != null)
            {
                mappedProduct = _mapper.Map<ProductViewModel>(product);
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



            }
            return mappedProduct;

        }

    }
}
