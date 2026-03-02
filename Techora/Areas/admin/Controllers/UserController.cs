using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Techora.Models;

namespace Techora.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        DevicesContext context;
        public UserController(DevicesContext ctx)
        {
            context = ctx;

        }
        public IActionResult AllUsersList()
        {

           
            UsersListModel usersList = new UsersListModel();


            usersList.lstCustomer = context.Customers.ToList();
            usersList.lstAdmin = context.Admins.ToList();
          
            return View(usersList);
        }
    }
}
