using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techora.Models;

namespace Bl
{
    
     public interface IAdmin
    {
        public List<Admin> GetAll();
       
        public Admin GetById(int? adminId);
      
        public bool Save(Admin admin);


    }
    public class ClsAdmins : IAdmin
    {
        DevicesContext context;
        public ClsAdmins(DevicesContext ctx)
        {
            context = ctx;

        }


        public List<Admin> GetAll()
        {
            try
            {



                var lstAdmins = context.Admins.ToList();
                return lstAdmins;
            }
            catch
            {
                return new List<Admin>();

            }

        }
      

        public Admin GetById(int? adminId)
        {
            try
            {


                var admin = context.Admins.FirstOrDefault(a => a.AdminId == adminId );
                return admin;
            }
            catch
            {
                return new Admin();

            }

        }




        public bool Save(Admin admin)
        {
            try
            {



                if (admin.AdminId == 0)// Save New Category
                {
                   
                    context.Admins.Add(admin);
                }
                else //Save edit Category
                {

                    admin.CreatedAt = DateTime.Now;
                    context.Entry(admin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                }

                context.SaveChanges();
                return true;
            }
            catch
            {

                return false;

            }

        }

    }
}


