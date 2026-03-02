using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Techora.Models;

public partial class Newsletter
{
    [Key]
    public int SubscriptionId { get; set; }

    public string? Email { get; set; }

    public DateTime? SubscribedAt { get; set; }
}
