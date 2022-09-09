using System.Collections.Generic;
using WebApplicationCrud.Models;

namespace WebApplicationCrud.ViewModels
{
    public class ShopViewModel
    {
        public List<Thumbnail> thumbnails { get; set; }
        public List<Product> products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public int? MaxPrice { get; set; }
        public List<string> Tags { get; set; }
        public string TagFilter { get; set; }
        public List<TextSize> TextSizes { get; set; }
        public string SelectedSize { get; set; }

        public int[] PriceList = new int[] { 50, 100, 150, 200, 250 };
       
        public string SearchString { get; set; }
    }
}
