using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Vinyl
{
    public int VinylId { get; set; }
    [Required]
    public string Title { get; set; } 
    public string? Description { get; set; } 
    [Required]
    public double Price { get; set; }
    
    // Navigation properties
    [Required]
    public int RecordLabelId { get; set; }
    public RecordLabel RecordLabel { get; set; }
    
    public ICollection<VinylGenre>? Genres { get; set; } 
    public ICollection<VinylCover>? AlbumCovers { get; set; } 
    public ICollection<Review> Reviews { get; set; }
}