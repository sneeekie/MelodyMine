using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class OrderProductDetails
{
    public int OrderProductDetailsId { get; set; }
    
    [Required]
    public int VinylId { get; set; } 

    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public double Price { get; set; }
    
    // Navigation properties
    [Required]
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public Vinyl Vinyl { get; set; }
}