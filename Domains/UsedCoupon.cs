using System;
using System.Collections.Generic;

namespace Techora.Models;

public partial class UsedCoupon
{
    public int UsedCouponId { get; set; }

    public int? CustomerId { get; set; }

    public int? CouponId { get; set; }

    public DateTime? UsedDate { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual Customer? Customer { get; set; }
}
