using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCrud.Models.BlogModels
{
    public class SubComment : Comment
    {
        public int MainCommentId { get; set; }
    }
}
