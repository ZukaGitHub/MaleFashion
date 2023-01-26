using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.ViewModels.HomeVMs
{
    public class CustomFiltersVM
    {
        
        public string[] CategoryNames { get; set; }
        public string[] BrandNames { get; set; }
        public string[] SelectedSizes { get; set; }
        public string[] SelectedTags { get; set; }
        public string FilterSort { get; set; }


        public int? MaxPrice { get; set; }
        public int? MinPrice { get; set; }

    }
}
