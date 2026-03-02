namespace Techora.Models
{
    public class ProFullDetailsShopModel
    {
     

        public List<ProductFullDetail> ProductAllShopDetail { get; set; }
        public List<Review> ReviewProducts { get; set; }
        public List<Product> RecommendedItems { get; set; }
        public List<ShoppingCartProductModel> ShoppingCartProductModel { get; set; }
       




        public ProFullDetailsShopModel()
        {

            ShoppingCartProductModel = new List<ShoppingCartProductModel>();
            if (ShoppingCartProductModel.Any())
            {
                
            }



        }
    }
}
