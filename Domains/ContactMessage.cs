using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Techora.Models;

public partial class ContactMessage
{
    [Key]
    public int MessageId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public DateTime? SentAt { get; set; }
}
