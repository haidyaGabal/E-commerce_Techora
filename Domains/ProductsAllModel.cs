using Microsoft.AspNetCore.Http;

namespace Techora.Models
{
    public class ProductsAllModel
    {
        internal List<ProductImage> ProductImages;

        public ProductImage PrImages { get; set; }
        public Product Product { get; set; }
        public ProductFullDetail ProductFullDetail { get; set; }
        public List<IFormFile> Files { get; set; } 


        public ProductsAllModel()

        {
            Files = new List<IFormFile>();
            Product = new Product();    
            ProductFullDetail = new ProductFullDetail();    
            Product = new Product();
            
        }
    }
}
