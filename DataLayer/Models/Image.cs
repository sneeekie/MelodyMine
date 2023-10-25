using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class VinylCover
{
    public int VinylCoverId { get; set; }
    [Required]
    public string Path { get; set; }
    
    // Navigation properties
    [Required]
    public int VinylId { get; set; }  // Foreign-Key
    public Vinyl Vinyl { get; set; }
}