using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebApplicationCrud.ViewModels
{
    public class ProductInfoViewModel
    {
        public int? Size { get; set; }
        public string SizeText { get; set; }
        public int Quantity { get; set; }
        public string color { get; set; }

        public List<IFormFile> Thumbnails { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
