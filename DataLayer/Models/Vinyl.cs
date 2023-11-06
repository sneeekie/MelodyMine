using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Vinyl
{
    public int VinylId { get; set; }
    [Required]
    public string Title { get; set; } 
    public string? Artist { get; set; } 
    [Required]
    public double Price { get; set; }
    [Required]
    public string ImagePath { get; set; }

    // Navigation properties
    public ICollection<VinylGenre>? VinylGenres { get; set; }
}