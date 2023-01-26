using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.Models.Identity
{
    public class DeliveryInfo
    {      
        public int Id { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Street { get; set; }
        public string AdditionalDescription { get; set; }
    }
}
