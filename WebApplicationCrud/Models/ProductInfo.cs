using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationCrud.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string ProductInfoThumbnailName { get; set; }
        public List<Image> Images { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public int ProductId { get; set; }
        public List<ProductInfoStockAndSize> ProductInfoStockAndSizes { get; set; }



    }
    public class ProductInfoStockAndSize
    {
        public int Id { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public int ProductInfoId { get; set; }
        public int ProductId { get; set; }
        public string SizeName { get; set; }
        public int Stock { get; set; }
    }
}
