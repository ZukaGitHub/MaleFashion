using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCrud.Models.BlogModels
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public string ImageName { get; set; }

        public string Message { get; set; }
        public DateTime Created { get; set; }
    }
}
