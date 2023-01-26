using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.ViewModels.CheckOutVMs
{
    public class DeliveryInfoViewModel
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Address { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string Street { get; set; }
      
        public string AdditionalDescription { get; set; }
    }
}
