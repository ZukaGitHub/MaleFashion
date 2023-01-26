namespace WebApplicationCrud.Models
{
    public class ShoppingCartItem
    {

        public int ShoppingCartItemId { get; set; }
        public Product Product { get; set; }
        public ProductInfo ProductInfo { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
        public string SizeText { get;  set; }
    }
}
