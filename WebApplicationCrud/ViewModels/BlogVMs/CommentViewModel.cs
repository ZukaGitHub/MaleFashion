using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCrud.ViewModels.BlogVMs
{
    public class CommentViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool IsPost { get; set; }
        [Required]
        public bool IsProduct { get; set; }
        [Required]
        public int MainCommentId { get; set; }
        [Required]
        public string Message { get; set; }
       
        public string ImageName { get; set; }

    }
}
