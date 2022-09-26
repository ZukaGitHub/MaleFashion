using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Models;

namespace WebApplicationCrud.ViewModels.PanelVMs
{
    public class EditProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public List<string> Tagnames { get; set; }
        public float Price { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string SalePercentage { get; set; }
        public List<ProductInfoEditVm> ProductInfos { get; set; }
    }

    public class ProductInfoEditVm
    {
        public string Color { get; set; }
        public List<ProductInfoStockAndSize> StockAndSize { get; set; }
        public List<string> ImageNames { get; set; }
        public string ThumbnailName{ get; set; }
    }
}
