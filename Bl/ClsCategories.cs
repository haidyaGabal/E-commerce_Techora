using Techora.Models;

namespace Techora.Bl
{
    public interface ICategory
    {
        public List<Category> GetAll();
        public List<Category> GetAllForShow();
        public Category GetById(int? CategoryId);
        public bool Delete(int? id);
        public bool Save(Category category);


    }
    public class ClsCategories:ICategory
    {
        DevicesContext context;
        public ClsCategories(DevicesContext ctx) 
        {
            context = ctx;

        }

        
        public List<Category> GetAll()
        {
            try
            {
                

                
                var lstCategories = context.Categories.Where(a => a.CurrentState == 1).ToList();
                return lstCategories;
            }
            catch
            {
                return new List<Category>();

            }

        }
        public List<Category> GetAllForShow()
        {
            try
            {



                var lstCategories = context.Categories.ToList();
                return lstCategories;
            }
            catch
            {
                return new List<Category>();

            }

        }

        public Category GetById(int? CategoryId)
        {
            try
            {

              
                var category = context.Categories.FirstOrDefault(a => a.CategoryId == CategoryId && a.CurrentState == 1);
                return category;
            }
            catch
            {
                return new Category();

            }

        }


        public bool Delete(int? id)
        {
            try
            {
             
                var category = GetById(Convert.ToInt32(id));
                // context.Category.Remove(category);

                category.CurrentState = 0;
                category.UpdatedBy = "Techora Admin";
                category.UpdatedDate = DateTime.Now;
                context.SaveChanges();


                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Save(Category category)
        {
            try
            {
                
               

                if (category.CategoryId == 0)// Save New Category
                {
                    category.UpdatedBy = "Techora Admin";
                    category.UpdatedDate = DateTime.Now;
                    category.CreatedDate = DateTime.Now;

                    context.Categories.Add(category);
                }
                else //Save edit Category
                {
                    category.UpdatedBy = "Techora Admin";
                    category.UpdatedDate = DateTime.Now;
                    category.CreatedDate = DateTime.Now;
                    context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
