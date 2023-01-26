using System;
using System.Collections.Generic;
using WebApplicationCrud.Models.Identity;

namespace WebApplicationCrud.Models
{
    public class Order
    {

        public int Id { get; set; }
        public ApplicationUser user { get; set; }
        public string SessionId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public float OrderTotal { get; set; }




    }
}
