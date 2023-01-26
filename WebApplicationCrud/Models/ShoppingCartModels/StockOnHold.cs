using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.Models.ShoppingCartModels
{
    public class StockOnHold
    {
        public int Id { get; set; }
        public int ProductInfoStockAndSizeId { get; set; }
        public string SessionId { get; set; }
        public ProductInfoStockAndSize ProductInfoStockAndSize { get; set; }
        public int Amount { get; set; }
        public DateTime ExpiryDate { get; set; } = DateTime.Now.AddMinutes(20);
    }
}
