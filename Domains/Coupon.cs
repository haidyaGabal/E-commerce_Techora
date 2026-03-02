using System;
using System.Collections.Generic;

namespace Techora.Models;

public partial class Coupon
{
    public int CouponId { get; set; }

    public string? Code { get; set; }

    public int? DiscountPercent { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public virtual ICollection<UsedCoupon> UsedCoupons { get; set; } = new List<UsedCoupon>();
}
