using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationCrud.Models;

namespace WebApplicationCrud.ViewModels
{
    public class AddProductVMget
    {
      
     
        public List<SelectListItem> Sizes { get; set; }
    
        public List<string> Sizes2 { get; set; }
    
    }
}
