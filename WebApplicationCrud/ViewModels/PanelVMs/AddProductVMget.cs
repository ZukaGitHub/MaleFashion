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


        public List<Brand> Brands { get; set; }
        public List<TextSize> Sizes { get; set; }
        public List<Category> Categories { get; set; }
        public int EditProductId { get; set; }

    }
}
