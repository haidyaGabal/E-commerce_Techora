using Bl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Techora.Bl;
using Techora.Models;

namespace Techora.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardAdminsController : Controller
    {
        IAdmin lstAdmin;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        DevicesContext _deviceContext;
        public DashboardAdminsController(IAdmin _admin, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, DevicesContext deviceContext)
        {
            lstAdmin = _admin;
            _userManager = userManager;
            _signInManager = signInManager;
            _deviceContext = deviceContext;
        }
        public IActionResult ShowAdmins()
        {

            return View(lstAdmin.GetAll());
        }
        public IActionResult AddAdmins()
        {

            return View();
        }

        public IActionResult EditAdmins(int? adminId)
        {


            var admin = new Admin();

            if (adminId != null)
            {
                admin = lstAdmin.GetById(adminId);
            }

            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Admin admin)
        {
            var admin2 = admin;
            ModelState.Remove("ApplicationUserId");

            if (!ModelState.IsValid)
                return View("EditAdmins", admin);





            ApplicationUser user = new ApplicationUser()
            {
                FirstName = admin.FullName,
                LastName = admin.FullName,
                Email = admin.Email,
                UserName = admin.Email,
            };


            try
            {
              
                var result = await _userManager.CreateAsync(user, admin.PasswordHash);
                if (result.Succeeded)
                {
                    //add to customer table
                admin2 = new Admin
                    {
                        ApplicationUserId = user.Id,
                        FullName = admin.FullName,
                        Email = admin.Email,
                        CreatedAt = DateTime.UtcNow,
                        PasswordHash = admin.PasswordHash,
                        RoleType=admin.RoleType,
                        PhoneNumber =admin.PhoneNumber,

                    };
                    _deviceContext.Admins.Add(admin2);
                    await _deviceContext.SaveChangesAsync();

                    // add role to customer (netRoleUser table )
                    var myUser = await _userManager.FindByEmailAsync(user.Email);
                    if (admin.RoleType== "DataEntry")
                    {
                        await _userManager.AddToRoleAsync(myUser, "Data Entry");

                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(myUser, "Admin");
                    }




                        var signIn = await _signInManager.PasswordSignInAsync(user, admin.PasswordHash, true, true);
                    //if (string.IsNullOrEmpty(model.ReturnUrl))
                    //{
                    //    return Redirect("~/");
                    //}
                    //else
                    //{
                    //    return Redirect(model.ReturnUrl);
                    //}
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }

            lstAdmin.Save(admin2);
            return View("AddAdmins") ;
        }

    }
}
