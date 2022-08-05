using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.ViewModels
{
    public class AddProductVMpost
    {

       
       
       
      

        public List<productVm> productVms { get; set; }
    }



    public class productVm
    {
       
        public string name { get; set; }
        public string category { get; set; }
        public string tagnames { get; set; }
       
        public float price { get; set; }
        public string brand { get; set; }
        public string description { get; set; }
        public string salePercentage { get; set; }
        public List<productInfoVm> productInfoVms { get; set; }
    }

    public class productInfoVm
    {
        public string color { get; set; }
        public string[] sizes { get; set; }

        public List<IFormFile> images { get; set; }
        public int Thumbnail { get; set; }
        public List<stockVm> stockVms { get; set; }


    }

    public class stockVm
    {

        public string sizeName { get; set; }
        public int number { get; set; }
    }
}
