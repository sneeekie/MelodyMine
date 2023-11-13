using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class OrderProductDetails
{
    public int OrderProductDetailsId { get; set; }
    
    [Required]
    public int VinylId { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
    
    public int Quantity { get; set; }
    
    // Navigation properties
    [Required]
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public Vinyl Vinyl { get; set; }
}