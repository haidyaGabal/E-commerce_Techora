
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Techora.Models;

namespace Techora.Bl
{
    public interface IProduct
    {
        public List<Product> GetAll();
        public List<Product> GetAllItemsData(int? categoryId);
        public List<Product> GetRecommendedItems(int itemId);
        public Product GetById(int? ItemId);
        public Product GetItemById(int? ItemId);
        public bool Delete(int? id);
        public bool Save(Product item);
        public List<Product> GetLatestProducts(int count);
        public List<Review> GetReviews();
        public List<Wishlist> GetWishlist();
        public List<ProductFullDetail> GetAllShopDetail(int? ItemId);
    }

    public class ClsProducts:IProduct
    {
        DevicesContext context;
       
        public ClsProducts(DevicesContext ctx)
        {
            context = ctx;

        }
        //Get All Category


       public List<Product> GetAll()
        {
            try
            {
                // Load products WITH their images
                var lstItems = context.Products
                                      .Include(p => p.ProductImages) // 👈 add this
                                      .ToList();
                return lstItems;
            }
            catch
            {
                return new List<Product>();
            }
            
        }
        public List<ProductFullDetail> GetAllShopDetail(int? ItemId)
        {
            try
            {
                // Load products WITH their images
                var lstItems = context.ProductFullDetails 
                                      .Include(p => p.Product)  
                                      .ThenInclude(r => r.ProductImages)
                                      .Where(a => ((a.ProductId == ItemId)))
                                      .ToList();
                return lstItems;
            }
            catch
            {
                return new List<ProductFullDetail>();
            }

        }
        public List<Review> GetReviews()
        {
            try
            {
                var lstItems = context.Reviews
                  .Include(r => r.Product)
                  .ThenInclude(p => p.ProductImages)
                  .ToList();

                return lstItems;
            }
            catch
            {
                return new List<Review>();
            }
        }
        public List<Wishlist> GetWishlist()
        {
            try
            {
                var lstItems = context.Wishlists
                  .Include(r => r.Product)
                  .ThenInclude(p => p.ProductImages)
                  .ToList();

                return lstItems;
            }
            catch
            {
                return new List<Wishlist>();
            }
        }


        public List<Product> GetLatestProducts(int count)
        {
            try
            {
                //or this .OrderByDescending(p => p.CreatedDate)
                var lstItems = context.Products.Include(p => p.ProductImages).OrderByDescending(p => p.ProductId).Take(count).ToList();
                return lstItems;
            }
            catch
            {
                return new List<Product>();
            }
        }

        public List<Product> GetAllItemsData(int? categoryId)
    {
        try
        {
            var lstItems = context.Products
                                  .Include(p => p.ProductImages) // 👈 add this
                                  .Where(a => ((a.CategoryId == categoryId) || categoryId == null || categoryId == 0)
                                               && a.CurrentState == 1
                                               && !string.IsNullOrEmpty(a.ProductName))
                                  .ToList();

            return lstItems;
        }
        catch
        {
            return new List<Product>();
        }
    }



    public List<Product> GetRecommendedItems(int itemId)
        {
            try
            {
                var item = GetById(itemId);

                var lstItems = context.Products
                    .Include(p => p.ProductImages) // ✅ load images
                    .Where(a =>( (a.Price > item.Price - 250 && a.Price < item.Price + 250))
                             && a.CurrentState == 1
                             && a.ProductId != itemId
                             && !string.IsNullOrEmpty(a.ProductName))
                    .Take(4)
                    .ToList();

                return lstItems;
            }
            catch
            {
                return new List<Product>();
            }
        }


    public Product GetById(int? ItemId)
        {
            try
            {
              

                var item = context.Products.Include(p => p.ProductFullDetail).Include(p => p.ProductImages).FirstOrDefault(a => a.ProductId == ItemId && a.CurrentState == 1);
                return item;
            }
            catch
            {
                return new Product();

            }

        }

        public Product GetItemById(int? ItemId)
        {
            try
            {

                
                var item = context.Products.FirstOrDefault(a => a.ProductId == ItemId && a.CurrentState == 1);
                return item;
            }
            catch
            {
                return new Product();

            }

        }







        public bool Delete(int? id)
        {
            try
            {
              
                var item = GetById(Convert.ToInt32(id));
                //context.Products.Remove(item);  ///delete from system without a trace
                item.CurrentState = 0;  ///delete but still exit
                context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();


                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Save(Product item)
        {
            try
            {
               

                item.CurrentState = 1;
                if (item.ProductId == 0) // new product
                {
                    context.Products.Add(item);
                }
                else
                {
                    var existing = context.Products
                                          .Include(p => p.ProductFullDetail)
                                          .Include(p => p.ProductImages)
                                          .FirstOrDefault(p => p.ProductId == item.ProductId);

                    if (existing != null)
                    {
                        // update product fields
                        context.Entry(existing).CurrentValues.SetValues(item);

                        // update detail safely
                        if (item.ProductFullDetail != null)
                        {
                            if (existing.ProductFullDetail == null)
                            {
                                existing.ProductFullDetail = item.ProductFullDetail;
                            }
                            else
                            {
                                existing.ProductFullDetail.Brand = item.ProductFullDetail.Brand;
                                existing.ProductFullDetail.Model = item.ProductFullDetail.Model;
                                existing.ProductFullDetail.Processor = item.ProductFullDetail.Processor;
                                existing.ProductFullDetail.Ram = item.ProductFullDetail.Ram;
                            }
                        }

                        // update images
                        foreach (var img in item.ProductImages ?? new List<ProductImage>())
                        {
                            if (img.ImageId == 0)
                                existing.ProductImages.Add(img);
                        }
                    }
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
