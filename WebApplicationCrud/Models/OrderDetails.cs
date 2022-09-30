﻿namespace WebApplicationCrud.Models
{
    public class OrderDetails
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int? Size { get; set; }
        public string SizeText { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }


    }
}
