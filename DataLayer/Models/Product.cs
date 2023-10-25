using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Product
{
    public int ProductId { get; set; }
    [Required]
    public string ProductName { get; set; }
    public string? ProductDescription { get; set; }
    [Required]
    public double Price { get; set; }
    
    // Navigation properties
    [Required]
    public int ManufacturerId { get; set; } // Foreign-Key
    public Manufacturer Manufacturer { get; set; }
    
    public ICollection<ProductCategory>? Categories { get; set; }
    public ICollection<Image>? Images { get; set; }
    public ICollection<Review> Reviews { get; set; }
}