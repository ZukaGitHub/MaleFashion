using System.Collections.Generic;
using WebApplicationCrud.Models;

namespace WebApplicationCrud.ViewModels
{
    public class HomeIndexViewModel
    {

        public List<Product> HotSales { get; set; }
        public Product GreatDeal { get; set; }
        public List<Product> BestSellers { get; set; }
        public List<Product> NewArrivals { get; set; }

    }
}
