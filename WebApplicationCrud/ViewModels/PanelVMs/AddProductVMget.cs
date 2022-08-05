using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationCrud.Models;

namespace WebApplicationCrud.ViewModels
{
    public class AddProductVMget
    {
        public int? SalePercentage { get; set; }
        [Newtonsoft.Json.JsonProperty("message")]
        public string SaleDeadLine { get; set; }
        public string[] Colors { get; set; }
        public int Count { get; set; }
      
        public int id { get; set; }
        [Newtonsoft.Json.JsonProperty("images")]
        public IFormCollection yle { get; set; }
        public string brand { get; set; }
      
        public List<Brand> BrandNames { get; set; }
        public List<SelectListItem> SelectedSizes { get; set; }
        public List<SelectListItem> Sizes { get; set; }
        public List<string> SelectedSizes2 { get; set; }
        public List<string> Sizes2 { get; set; }
        public int? stock { get; set; }
        public string TagNames { get; set; }
        public List<ViewModels.ProductInfoViewModel> ProductInfos { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string desc { get; set; }
    }
}
