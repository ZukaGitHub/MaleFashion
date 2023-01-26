using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.ViewModels.HomeVMs
{
    public class HomeIndexViewModel
    {
        public List<ProductViewModel> NewArrivals { get; set; }
        public List<ProductViewModel> HotSales{ get; set; }
        public List<ProductViewModel> BestSellers { get; set; }
        public ProductViewModel HotSale { get; set; }
    }
}
