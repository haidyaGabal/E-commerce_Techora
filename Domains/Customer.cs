using System;
using System.Collections.Generic;


namespace Techora.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string ApplicationUserId { get; set; } 

        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Email { get; set; } = null!;
        public string? PasswordHash { get; set; }
       


        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<UsedCoupon> UsedCoupons { get; set; } = new List<UsedCoupon>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

       
    }

}
