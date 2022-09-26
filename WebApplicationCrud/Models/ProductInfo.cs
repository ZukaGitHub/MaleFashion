using System.Collections.Generic;

namespace WebApplicationCrud.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string ProductInfoThumbnailName { get; set; }
        public List<Image> Images { get; set; }
        public int ProductId { get; set; }
        public List<ProductInfoStockAndSize> ProductInfoStockAndSizes { get; set; }



    }
    public class ProductInfoStockAndSize
    {
        public int Id { get; set; }
        public int ProductInfoId { get; set; }
        public int ProductId { get; set; }
        public string SizeName { get; set; }
        public int Stock { get; set; }
    }
}
