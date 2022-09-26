using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Models;

namespace WebApplicationCrud.ViewModels
{
    public class AddProductVMpost
    { 
        public List<ProductVm> Products { get; set; }
    }



    public class ProductVm
    {       
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Tagnames { get; set; }       
        public float Price { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string SalePercentage { get; set; }
        public List<ProductInfoVm> ProductInfos { get; set; }
    }

    public class ProductInfoVm
    {
        public string Color { get; set; }
     
        public List<Image> Images { get; set; }       
        public List<StockVm> Stock { get; set; }
        public List<string> ImageNames { get; set; }
        public int? ThumbnailEditIndex { get; set; }

    }

    public class StockVm
    {

        public string SizeName { get; set; }
        public int Number { get; set; }
    }
}
