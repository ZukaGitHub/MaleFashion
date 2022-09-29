using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Models.BlogModels;

namespace WebApplicationCrud.ViewModels.HomeVMs
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float? PreviousPrice { get; set; }
        public int? SalePercentange { get; set; }
        public string Description { get; set; }
        public DateTime? SaleTimer { get; set; }
        public List<string> Tags { get; set; }
        public List<MainComment> Comments { get; set; }
        public List<ProductViewModel> RelatedProducts { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public List<ProductInfoViewModel> ProductInfos { get; set; }



    }
}
