using System.Collections.Generic;
using WebApplicationCrud.Models;
using WebApplicationCrud.Models.BlogModels;

namespace WebApplicationCrud.ViewModels
{
    public class ProductPanelViewModel
    {
        public int ProductId { get; set; }
        public List<RelatedProductViewModel> RelatedProducts { get; set; }
        public string Brand { get; set; }
        public List<string> Images { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Tags { get; set; }
        public List<MainComment> Comments { get; set; }

        public string Category { get; set; }
        public string Desc { get; set; }
    }
}
