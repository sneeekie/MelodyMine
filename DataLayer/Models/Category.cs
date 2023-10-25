using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Category
{
    public int CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; }
    
    // Navigation property
    public ICollection<ProductCategory> Products { get; set; }
}