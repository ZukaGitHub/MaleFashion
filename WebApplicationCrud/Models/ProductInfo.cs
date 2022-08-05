using System.Collections.Generic;

namespace WebApplicationCrud.Models
{
    public class ProductInfo
    {
        public int id { get; set; }
        public string color { get; set; }
        public int ThumbnailIndex { get; set; }
        public List<Image> Images { get; set; }
        public int ProductId { get; set; }
        public List<ProductInfoStockAndSize> ProductInfoStockAndSizes { get; set; }



    }
    public class ProductInfoStockAndSize
    {
        public int id { get; set; }
        public int ProductInfoId { get; set; }
        public int ProductId { get; set; }
        public string SizeName { get; set; }
        public int stock { get; set; }
    }
}
