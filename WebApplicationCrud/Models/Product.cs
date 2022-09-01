using System.Collections.Generic;
using WebApplicationCrud.Models.BlogModels;

namespace WebApplicationCrud.Models
{
    public class Product

    {

        public int id { get; set; }
        public Brand brand { get; set; }
        public string BrandName { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ProductInfo> ProductInfos { get; set; }
        public List<MainComment> Comments { get; set; }
        public int? SalePercentage { get; set; }
        public string OwnerId { get; set; }
        public int stock { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public float? NewPrice { get; set; }

        public string CategoryName { get; set; }
        public string desc { get; set; }


    }
}
