using WebApplicationCrud.Models;

namespace WebApplicationCrud.ViewModels
{
    public class EditProductViewModel
    {
        public Brand brand { get; set; }
        public string BrandName { get; set; }
        public int? SalePercentage { get; set; }

        public int stock { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public float? NewPrice { get; set; }

        public string CategoryName { get; set; }
        public string desc { get; set; }
    }

}
