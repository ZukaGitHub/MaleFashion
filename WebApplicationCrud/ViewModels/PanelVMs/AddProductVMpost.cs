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
        public List<productVm> Products { get; set; }
    }



    public class productVm
    {       
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Tagnames { get; set; }       
        public float Price { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string SalePercentage { get; set; }
        public List<productInfoVm> ProductInfos { get; set; }
    }

    public class productInfoVm
    {
        public string Color { get; set; }
     
        public List<Image> Images { get; set; }       
        public List<stockVm> Stock { get; set; }


    }

    public class stockVm
    {

        public string sizeName { get; set; }
        public int number { get; set; }
    }
}
