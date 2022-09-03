using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Models.BlogModels;

namespace WebApplicationCrud.ViewModels.BlogVMs
{
    public class BlogDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string Author { get; set; } = "";
        public string Quote { get; set; } = "";
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string QuoteAuthor { get; set; }
        public List<MainComment> Comments { get; set; }
        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public string Category { get; set; } = "";
        public string Image { get; set; } = "";
        public int  CommentCount { get; set; }
        public List<RelatedBlogsViewModel> RelatedBlogs { get; set; }

    }
}
