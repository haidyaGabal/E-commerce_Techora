using System;
using System.Collections.Generic;

namespace Techora.Models;

public partial class Admin
{
    public int AdminId { get; set; }
    public string ApplicationUserId { get; set; }
    public string? FullName { get; set; }
    public string RoleType { get; set; }
    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }
    public string? PhoneNumber { get; set; }
 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
