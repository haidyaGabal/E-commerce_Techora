using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Techora.Bl;
using Techora.Models;


namespace Techora.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize (Roles= "Admin,Data Entry")]
    public class CategoriesController : Controller
    {
        ICategory lstCategories;
        public CategoriesController(ICategory category)
        {
            lstCategories = category;

        }

        
        public IActionResult OverList()
        {


            return View(lstCategories.GetAllForShow());
        }
      
        public IActionResult CateEdit()
        {


            return View(lstCategories.GetAll());
        }

        public IActionResult Edit(int? CategoryId)
        {

        
            var category = new Category();
     
            if (CategoryId != null)
            {
                category = lstCategories.GetById(CategoryId);
            }

            return View(category);
        }


        public IActionResult Delete(int CategoryId)
        {
            lstCategories.Delete(CategoryId);
            return RedirectToAction("CateEdit");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAsync(Category category, List<IFormFile> Files)
        {


            if (!ModelState.IsValid)
                return View("Edit", category);
            category.ImageName = await UploadImage(Files);
            lstCategories.Save(category);
            return RedirectToAction("CateEdit");
        }



        private async Task<string> UploadImage(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return null;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/CateoryImges");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(files[0].FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await files[0].CopyToAsync(stream);
            }

            return fileName; // only store the name in DB
        }

    }
}
