using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Techora.Models;

public partial class Category
{
    [ValidateNever]
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "please enter Category name")]
    public string CategoryName { get; set; }
    [ValidateNever]
    public string ?CreatedBy { get; set; } 
    [ValidateNever]
    public DateTime? CreatedDate { get; set; }

    public int CurrentState { get; set; } = 1;
    [ValidateNever]
    public string? ImageName { get; set; } 


    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
