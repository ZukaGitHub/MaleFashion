using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.Models
{
    public class UserRating
    {
        public string UserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Rate { get; set; }
    }
}
