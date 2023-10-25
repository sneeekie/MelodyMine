using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Review
{
    public int ReviewId { get; set; }
    public string? ReviewComment { get; set; }
    [Required]
    [Range(1, 5)]
    public int NumStars { get; set; }
    
    // Navigation properties
    [Required]
    public int VinylId { get; set; }  // Foreign-Key
    public Vinyl Vinyl { get; set; }
}