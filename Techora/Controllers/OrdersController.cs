using Bl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Techora.Models;

public class OrdersController : Controller
{
    private readonly ICart _cartService;
    private readonly DevicesContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrdersController(ICart cartService, DevicesContext context, UserManager<ApplicationUser> userManager)
    {
        _cartService = cartService;
        _context = context;
        _userManager = userManager;
    }

    // 🔹 Show Shopping Cart (main page)
    public async Task<IActionResult> ShoppingCart()
    {
        var shoppingCart = await GetShoppingCartAsync();
        return View(shoppingCart);
    }

    // 🔹 Alternative Cart page (if you use a different view)
    [Authorize(Roles = "Customer")]
    public IActionResult Cart()
    {
        int customerId = GetCurrentCustomerId();
        var cartItems = _cartService.GetCartByCustomer(customerId);
        var shoppingCart = BuildShoppingCart(cartItems);

        // Restore promo code if any
        var promoCode = TempData["PromoCode"] as string;
        if (!string.IsNullOrEmpty(promoCode))
        {
            shoppingCart.PromoCode = promoCode;

            if (promoCode=="Techora5")
            {
                var discountPercent = 10m;
                shoppingCart.Total = shoppingCart.SubTotal - ((discountPercent / 100m) * shoppingCart.SubTotal);
            }
            else
            {
                shoppingCart.Total = shoppingCart.SubTotal;
            }
        }
        else
        {
            shoppingCart.Total = shoppingCart.SubTotal;
        }

        return View(shoppingCart);
    }



    // 🔹 Add item to cart


    [HttpPost]
    public IActionResult AddToCart(int productId, int quantity, string promoCode)
    {
        var userId = _userManager.GetUserId(User);

        if (string.IsNullOrEmpty(userId))
        {
            // user not logged in → go to login, then back to Cart page
            return RedirectToAction("Login", "Account", new { ReturnUrl = Url.Action("Cart", "Orders") });
        }

        int customerId = GetCurrentCustomerId();
        var result = _cartService.AddToCart(customerId, productId, quantity);


        if (!result)
            return BadRequest("Could not add to cart.");



        if (!string.IsNullOrEmpty(promoCode))
        {
            TempData["PromoCode"] = promoCode;
        }


        return RedirectToAction(nameof(Cart));
    }

 





    // 🔹 Remove item from cart
    [HttpGet]
    public IActionResult Remove(int cartId)
    {
        // Optionally: check result boolean from service
        var removed = _cartService.RemoveFromCart(cartId);
        // you might return BadRequest if removed == false

        return RedirectToAction(nameof(Cart));
    }




    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var userId = _userManager.GetUserId(User);
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

        if (customer == null)
            return RedirectToAction("Login", "Account");

        var carts = await _context.Carts
            .Include(c => c.Product)
            .Where(c => c.CustomerId == customer.CustomerId)
            .ToListAsync();

        return View(carts);
    }

    [HttpPost]
    [Authorize (Roles ="Customer")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(int[] CartIds, string PaymentMethod)
    {
        var userId = _userManager.GetUserId(User);
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

        if (customer == null)
            return RedirectToAction("Login", "Account");

        var carts = await _context.Carts
            .Include(c => c.Product)
            .Where(c => CartIds.Contains(c.CartId) && c.CustomerId == customer.CustomerId)
            .ToListAsync();

        if (!carts.Any())
            return RedirectToAction("Cart");

        var order = new Order
        {
            CustomerId = customer.CustomerId,
            OrderDate = DateTime.Now,
            TotalAmount = carts.Sum(c => c.Product.Price * c.Quantity),
            Status = "Pending",
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        foreach (var cart in carts)
        {
            _context.OrderDetails.Add(new OrderDetail
            {
                OrderId = order.OrderId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                Price = cart.Product.Price
            });
        }

        _context.Carts.RemoveRange(carts);
        await _context.SaveChangesAsync();
        TempData["OrderSuccess"] = "Your order has been placed successfully!";

        return RedirectToAction("Index", "Home");

    }




    // ================= HELPER METHODS =================

    private int GetCurrentCustomerId()
    {
        var userId = _userManager.GetUserId(User);

        if (string.IsNullOrEmpty(userId))
            return 0;
        var customer = _context.Customers.FirstOrDefault(c => c.ApplicationUserId == userId);

        

        return customer.CustomerId;
    }

    private async Task<ShoppingCart> GetShoppingCartAsync()
    {
        int customerId = GetCurrentCustomerId();
        var cartItems = await Task.Run(() => _cartService.GetCartByCustomer(customerId));
        return BuildShoppingCart(cartItems);
    }

    private ShoppingCart BuildShoppingCart(List<Cart> cartItems)
    {
        return new ShoppingCart
        {
            Products = cartItems.Select(c => new ShoppingCartProductModel
            {
                // <-- make sure this is the cart PK property name in your Cart entity
                CartId = c.CartId,
                ProductId = c.ProductId ?? 0,
                ProductName = c.Product?.ProductName ?? "Unknown",
                Price = c.Product?.Price ?? 0,
                Qty = c.Quantity,
                ImageUrl = c.Product?.ProductImages?.FirstOrDefault()?.ImageUrl
            }).ToList(),

            SubTotal = cartItems.Sum(c => c.Quantity * (c.Product?.Price ?? 0)),
            Total = cartItems.Sum(c => c.Quantity * (c.Product?.Price ?? 0))
        };
    }

}
