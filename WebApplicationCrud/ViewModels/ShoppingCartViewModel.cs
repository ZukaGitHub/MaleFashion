using WebApplicationCrud.Models;

namespace WebApplicationCrud.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public float ShoppingCartTotal { get; set; }
        public int ShoppingCartItemsAmount { get; set; }

    }
}
