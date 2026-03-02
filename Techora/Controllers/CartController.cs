using Bl;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Techora.Models;

namespace Techora.Controllers
{
    public class CartController : Controller
    {
         ICart _cartService;
        DevicesContext _context;
        UserManager<ApplicationUser> _userManager;

        public CartController(ICart cartService, DevicesContext context, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _context = context;
            _userManager = userManager;
        }

        private int GetCurrentCustomerId()
        {
            var userId = _userManager.GetUserId(User);
            var customer = _context.Customers.FirstOrDefault(c => c.ApplicationUserId == userId);

            if (customer == null)
                throw new Exception("Customer not found for logged-in user.");

            return customer.CustomerId;
        }

        // Show cart items
        public IActionResult Index()
        {
            int customerId = GetCurrentCustomerId();
            var cartItems = _cartService.GetCartByCustomer(customerId);
            return View(cartItems);
        }

    }
}
