using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Vinyl
{
    public int VinylId { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } 
    
    [Required(ErrorMessage = "Artist is required")]
    public string? Artist { get; set; }
    
    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Image path is required")]
    public string ImagePath { get; set; }
    
    [Required(ErrorMessage = "Genre is required")]
    public int? GenreId { get; set; }


    // Navigation properties
    public ICollection<VinylGenre>? VinylGenres { get; set; }
}