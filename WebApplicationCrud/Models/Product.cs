using System;
using System.Collections.Generic;
using WebApplicationCrud.Models.BlogModels;

namespace WebApplicationCrud.Models
{
    public class Product
      
{
        public int Id { get; set; }
        public Brand Brand { get; set; }
        public string BrandName { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ProductInfo> ProductInfos { get; set; }
        public List<Image> Images { get; set; }
        public List<MainComment> Comments { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public int? SalePercentage { get; set; }
        public float? StarRate { get; set; }
        public string OwnerId { get; set; }      
        public string Name { get; set; }
        public float Price { get; set; }
        public float? NewPrice { get; set; }
        public Category Category { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime TimeAdded { get; set; }
        public bool DisplayState { get; set; } = true;
        public  int? TimesSold { get; set; }


    }
}
