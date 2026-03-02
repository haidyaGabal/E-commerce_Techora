using Bl;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Techora.Models;

namespace Techora.Controllers
{
    public class AccountController : Controller
    {
   
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        DevicesContext _deviceContext;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, DevicesContext deviceContext)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _deviceContext = deviceContext;
        }
        public IActionResult Login(string returnUrl)
        {
            UserModel model = new UserModel();
            model.ReturnUrl = returnUrl;
            return View(model);
        }
        
        public IActionResult AccessDenied()
        {
            
            return View();
        }
        public IActionResult Register()
        {
            return View(new UserModel()); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModel model)
        {
            ModelState.Remove("ReturnUrl");
            if (!ModelState.IsValid)
                return View("Register",model);

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = $"{model.FirstName} {model.LastName}",
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //add to customer table
                    var customer = new Customer
                    {
                        ApplicationUserId = user.Id,
                        FullName = $"{model.FirstName} {model.LastName}",
                        Email=model.Email,
                        CreatedAt = DateTime.UtcNow,
                        PasswordHash=model.Password,
                        Address="Cairo",
                        PhoneNumber="011-223"
                       
                    };
                    _deviceContext.Customers.Add(customer);
                    await _deviceContext.SaveChangesAsync();

                    // add role to customer (netRoleUser table )

                    var myUser = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.AddToRoleAsync(myUser, "Customer");

                    var signIn = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                    if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        return Redirect(model.ReturnUrl);
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }




            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel model)
        {
            ///this means  ModelState.Remove("FirstName");ModelState.Remove("LastName"); ModelState.Remove("ConfirmPassword");
            /// that not requird

            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("ReturnUrl");

            if (!ModelState.IsValid)
                return View("Login", model);

            ApplicationUser user = new ApplicationUser()
            {
                
                Email = model.Email,
                UserName = model.Email,
            };
            try
            {
                
                    var signIn = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
                if (signIn.Succeeded)
                {
                    if(string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        return Redirect(model.ReturnUrl);
                    }
                        
                }

            }
            catch (Exception ex)
            {

            }




            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");

        }

    }
}
