using WebApplicationCrud.Models;
using WebApplicationCrud.Models.BlogModels;
using WebApplicationCrud.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCrud.ViewModels.BlogVMs;

namespace WebApplicationCrud.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        FrontPostViewModel GetFrontPost(int id);
        List<Post> GetAllPosts();
        IndexViewModel GetAllPosts(int pageNumber, string category, string search);
        void AddPost(Post post);
        void UpdatePost(Post post);
        List<RelatedBlogsViewModel> GetPrevAndNextPosts(int id);
        public int PageCountValidity(int pageNumber);
        void UpdateProduct(Product product);
        Product GetProduct(int id);
        void RemovePost(int id);
        void AddSubComment(SubComment comment);
        Task<bool> SaveChangesAsync();
    }
}
