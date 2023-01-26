using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebApplicationCrud.ViewModels.HomeVMs
{
    public class ProductInfoViewModel
    {
        public int? Id { get; set; }
        public List<string> ImageNames { get; set; }
        public string ProductInfoThumbnailName { get; set; }
        public string Color { get; set; }
        public List<StockVm> Stock { get; set; }

    }
}
