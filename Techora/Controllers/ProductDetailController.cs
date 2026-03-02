using Microsoft.AspNetCore.Mvc;
using Techora.Bl;
using Techora.Models;

namespace Techora.Controllers
{
    public class ProductDetailController : Controller
    {
        IProduct lstProduct;
        public ProductDetailController(IProduct product)
        {

            lstProduct = product;

        }
        public IActionResult ProductFullDetails(int ProductId)
        {
            var model = new ProFullDetailsShopModel
            {
                ProductAllShopDetail = lstProduct.GetAllShopDetail(ProductId),
                ReviewProducts = lstProduct.GetReviews(),
                RecommendedItems=lstProduct.GetRecommendedItems(ProductId)

            };

            return View(model);
        }

    }
}
