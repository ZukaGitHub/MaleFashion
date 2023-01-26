using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Models.Identity;

namespace WebApplicationCrud.ViewModels.CheckOutVMs
{
    public class CheckOutViewModel
    {
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
        [Required]
        public DeliveryInfo DeliveryInfo { get; set; }
    }
}
