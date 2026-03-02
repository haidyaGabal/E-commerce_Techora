using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Techora.Models;
namespace Bl
{
    public class ApplicationUser: IdentityUser

    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
    }
}
