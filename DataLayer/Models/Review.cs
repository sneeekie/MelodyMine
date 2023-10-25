using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Review
{
    public int ReviewId { get; set; }
    public string? ReviewComment { get; set; }
    [Required]
    public int NumStars { get; set; }
    
    // Navigation properties
    public int ProductId { get; set; }  // Foreign-Key
    public Product Product { get; set; }
}