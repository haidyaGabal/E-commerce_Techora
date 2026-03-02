using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Techora.Models;
namespace Techora.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class ShowOrdersController : Controller
    {
      
       
            DevicesContext context;
            public ShowOrdersController(DevicesContext ctx)
            {
                context = ctx;

            }
            // Make sure you have this

    public IActionResult TableOfOrders()
    {
        AllOrdersModel allOrdersModel = new AllOrdersModel();

        allOrdersModel.lstOrder = context.Orders
            .Include(o => o.Customer)   // load related Customer
            .ToList();

        allOrdersModel.lstOrderDetails = context.OrderDetails.ToList();

        return View(allOrdersModel);
    }

}
    }

