using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Techora.Bl;
using Techora.Models;

namespace Techora.Controllers
{
    public class HomeController : Controller
    {
        ICategory lstCategories;
        IProduct lstProduct;
        public HomeController(ICategory category,IProduct product)
        {
            lstCategories = category;
            lstProduct = product;

        }

        public IActionResult Index(int? categoryId, int? productId)
        {
            var model = new CustomersCatsItemsModel
            {
                lstCategory = lstCategories.GetAll(),
                lstProduct = lstProduct.GetAllItemsData(categoryId),
                LatestProducts = lstProduct.GetLatestProducts(6),
                ReviewProducts = lstProduct.GetReviews(),
                Wishlists=lstProduct.GetWishlist()
            };

            return View(model);
        }






        public IActionResult Privacy()
        {
            return View();
        }

     
    }
}
