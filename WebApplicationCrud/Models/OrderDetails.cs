using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationCrud.Models
{
    public class OrderDetails
    {
       
        public int Id { get; set; }      
        public string SizeText { get; set; }
        public int Quantity { get; set; }     
        public int ProductInfoId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public ProductInfo ProductInfo { get; set; }
        public Order Order { get; set; }
       
        //relationships are fucked

    }
}
