

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Techora.Bl;
using Techora.Models;
using Techora.Resources;

namespace Techora.Areas.admin.Controllers
{

    [Area("admin")]
    [Authorize(Roles = "Admin,Data Entry")]
    public class ItemsController : Controller
    {
        ICategory oClsCategories;
        IProduct oClsItems;
        public ItemsController(IProduct product,ICategory category)
        {
            oClsItems = product;
            oClsCategories = category;


        }

     
      
    
      

        public IActionResult ProductList(int? categoryId)
        {
            ViewBag.lstCategories = oClsCategories.GetAll();
          
            var items = oClsItems.GetAllItemsData(categoryId);


            return View(items);
        }



        public IActionResult Search(int id)
        {
            ViewBag.lstCategories = oClsCategories.GetAll();
            var items = oClsItems.GetAllItemsData(id);


            return View("ProductList", items);
        }

        public IActionResult Edit(int? ProductId)
        {
            ViewBag.lstCategories = oClsCategories.GetAll();
           
          

            var item = new Product
            {
                ProductFullDetail = new ProductFullDetail()
            };

            if (ProductId != null)
            {
                item = oClsItems.GetById(Convert.ToInt32(ProductId));
                item.ProductFullDetail ??= new ProductFullDetail();
            }

            return View(item);
        }





        public IActionResult Delete(int ProductId)
        {
            oClsItems.Delete(ProductId);
            return RedirectToAction("ProductList");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
 
        public async Task<IActionResult> Save(Product product, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", product);

            var fileNames = await UploadImages(Files);

            if (fileNames.Any())
            {
                product.ProductImages ??= new List<ProductImage>();

                foreach (var fileName in fileNames)
                {
                    product.ProductImages.Add(new ProductImage
                    {
                        ImageUrl = "/Admin/ProductImages/" + fileName

                    });
                }
            }

            oClsItems.Save(product);
            return RedirectToAction("ProductList", new { categoryId = product.CategoryId });

        }



        private async Task<List<string>> UploadImages(List<IFormFile> files)
        {
            var savedFiles = new List<string>();

            if (files == null || files.Count == 0)
                return savedFiles;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/ProductImages");
            Directory.CreateDirectory(uploadsFolder);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    savedFiles.Add(fileName);
                }
            }

            return savedFiles;
        }







    }
}
