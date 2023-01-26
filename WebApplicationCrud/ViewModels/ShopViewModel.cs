using System.Collections.Generic;
using WebApplicationCrud.Models;
using WebApplicationCrud.ViewModels.HomeVMs;

namespace WebApplicationCrud.ViewModels
{
    public class ShopViewModel
    {
       
        public List<ProductViewModel> products { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Brands { get; set; }     
        public List<string> Tags { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public List<string> TextSizes { get; set; }
        public int[] PriceList = new int[] { 50, 100, 150, 200, 250 };      
        public string SearchString { get; set; }
    }
}
