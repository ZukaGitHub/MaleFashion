using System.Collections.Generic;
using WebApplicationCrud.Models;

namespace WebApplicationCrud.ViewModels
{
    public class ProductPanelViewModel
    {
        public int? ProductId { get; set; }
        public List<RelatedProductViewModel> RelatedProducts { get; set; }
        public string brand { get; set; }
        public List<Image> Images { get; set; }
        public int stock { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string Tags { get; set; }

        public string category { get; set; }
        public string desc { get; set; }
    }
}
