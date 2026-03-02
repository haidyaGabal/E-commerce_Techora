namespace Techora.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartProductModel> Products { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PromoCode { get; set; } = "Techora5";


        public ShoppingCart()
        {
            Products = new List<ShoppingCartProductModel>();
        }
    }
}
