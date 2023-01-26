using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public DeliveryInfo DeliveryInfo { get; set; }
        public List<Order> Orders { get; set; }
        public List<FavouriteProduct> FavouriteProducts { get; set; }
    }
}
