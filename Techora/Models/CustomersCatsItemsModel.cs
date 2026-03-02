namespace Techora.Models
{
    public class CustomersCatsItemsModel
    {
        public List<Customer> lstCustomer { get; set; }
        public List<Category> lstCategory { get; set; }
        public List<Product> lstProduct { get; set; }
        public List<Product> LatestProducts { get; set; }
        public List<Review> ReviewProducts {  get; set; }
        public List<Wishlist> Wishlists { get; set; }
        public CustomersCatsItemsModel()

        {
            lstCustomer = new List<Customer>();
            lstCategory = new List<Category>();
            lstProduct = new List<Product>();
        }
    }
}
