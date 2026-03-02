namespace Techora.Models
{
    public class ShoppingCartProductModel
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; } 
        public decimal PricePerProduct { get; set; }


    }
}
