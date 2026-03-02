using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Techora.Models;

namespace Techora.Areas.admin.Controllers
{
   
    [Area("admin")]
    [Authorize(Roles = "Admin,Data Entry")]
    public class HomeController : Controller
    {
        DevicesContext context;
        public HomeController(DevicesContext ctx)
        {
            context = ctx;

        }
        public IActionResult Index()
        {
    

       
            CustomersCatsItemsModel customersCatsItemsModel = new CustomersCatsItemsModel();


            customersCatsItemsModel.lstCustomer = context.Customers.ToList();
            customersCatsItemsModel.lstCategory = context.Categories.ToList();
            customersCatsItemsModel.lstProduct = context.Products?.ToList() ?? new List<Product>();

            return View(customersCatsItemsModel);
        }
    }
}
