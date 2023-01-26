using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.Models.Identity
{
    public class FavouriteProduct
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ProductId { get; set; }

    }
}
