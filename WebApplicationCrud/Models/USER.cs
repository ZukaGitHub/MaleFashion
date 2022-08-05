using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationCrud.Models
{
    public class USER
    {
       
        public string firstname { get; set; }
       
        public string lastname { get; set; }
   
        public List<IFormFile> images { get; set; }
    }
}
