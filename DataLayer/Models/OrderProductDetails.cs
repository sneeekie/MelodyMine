using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class OrderProductDetails
{
    public int OrderProductDetailsId { get; set; }
    
    [Required]
    public int VinylId { get; set; } 

    [Required]
    public string Title { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    // Navigation properties
    [Required]
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public Vinyl Vinyl { get; set; }
}