
using WebApplicationCrud.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Models.BlogModels;
using WebApplicationCrud.ViewModels.BlogVMs;
using WebApplicationCrud.Data.Repository;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Data.Helpers;

namespace WebApplicationCrud.Data.Repository
{
    public class Repository : IRepository
    {
        private CRUDdbcontext _ctx;

        public Repository(CRUDdbcontext ctx)
        {
            _ctx = ctx;
        }

        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _ctx.Posts.ToList();
        }
        public int PageCountValidity(int pageNumber)
        {
            int pageSize = 3;
            var query = _ctx.Posts.AsNoTracking().AsQueryable();
            int postsCount = query.Count();
            int pageCount = (int)Math.Ceiling((double)postsCount / pageSize);

            if (pageNumber < 1)
            {
                return 1;
            }
            if (pageNumber > pageCount)
            {
                return pageCount;
            }
            return pageNumber;
        }
        public IndexViewModel GetAllPosts(
            int pageNumber, 
            string category,
            string search)
        {
            Func<Post, bool> InCategory = 
                (post) => 
                { return post.Category.ToLower().Equals(category.ToLower()); };

            int pageSize = 3;
            int skipAmount = pageSize * (pageNumber - 1);

            var query = _ctx.Posts.Include(com=>com.MainComments).ThenInclude(subcom=>subcom.SubComments).AsNoTracking().AsQueryable();

            if (!String.IsNullOrEmpty(category))
                query = query.Where(x => InCategory(x));

            if (!String.IsNullOrEmpty(search))
                query = query.Where(x => EF.Functions.Like(x.Title, $"%{search}%")
                                    || EF.Functions.Like(x.Body, $"%{search}%") 
                                    || EF.Functions.Like(x.Description, $"%{search}%"));

            int postsCount = query.Count();
            int pageCount = (int)Math.Ceiling((double)postsCount / pageSize);

         
            return new IndexViewModel
            {
                PageNumber = pageNumber,
                PageCount = pageCount,
                NextPage = postsCount > skipAmount + pageSize,
                Pages = PageHelper.PageNumbers(pageNumber, pageCount).ToList(),
                Category = category,
                Search = search,
                Posts = query
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToList()
            };
        }

        public Post GetPost(int id)
        {
            return _ctx.Posts
                .Include(p => p.MainComments)
                    .ThenInclude(mc => mc.SubComments)
                .FirstOrDefault(p => p.Id == id);
        }
        public Product GetProduct(int id)
        {
            return _ctx.Products.Include(p => p.Comments)
                    .ThenInclude(mc => mc.SubComments)
                .FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }
        public void UpdateProduct(Product product)
        {
            _ctx.Products.Update(product);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void AddSubComment(SubComment comment)
        {
            _ctx.SubComments.Add(comment);
        }

        public FrontPostViewModel GetFrontPost(int id)
        {
            return _ctx.Posts
                   .Include(p => p.MainComments)
                       .ThenInclude(mc => mc.SubComments)
                   .Select(x => new FrontPostViewModel
                   {
                       Id = x.Id,
                       Title = x.Title,
                       Description = x.Description,
                       Body = x.Body,

                   })
                   .FirstOrDefault(p => p.Id == id);
        }

        public List<RelatedBlogsViewModel> GetPrevAndNextPosts(int id)
        {
            var relatedPosts = new List<RelatedBlogsViewModel>();
            var prevPost = _ctx.Posts.Where(s => s.Id < id)?.OrderByDescending(x => x.Id).FirstOrDefault();

           var nextPost= _ctx.Posts.Where(s => s.Id > id)?.OrderBy(x => x.Id).FirstOrDefault();

           
            if (prevPost != null)
            {
                relatedPosts.Add(new RelatedBlogsViewModel()
                {
                    Id = prevPost.Id,
                    IsPrev = true,
                    Title = prevPost.Title,
                
                });
             
            }
            if (nextPost != null)
            {
                relatedPosts.Add(new RelatedBlogsViewModel()
                {
                    Id = nextPost.Id,
                    IsPrev = false,
                    Title = nextPost.Title,

                });
            }


            return relatedPosts;
        }
    }
}
