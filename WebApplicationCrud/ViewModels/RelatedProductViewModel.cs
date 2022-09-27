using System.Collections.Generic;
using WebApplicationCrud.Models;

namespace WebApplicationCrud.ViewModels
{
    public class RelatedProductViewModel
    {
        public int ProductId { get; set; }      
        public float Price { get; set; }

        public List<ProductInfoVm> MyProperty { get; set; }


    }
}
