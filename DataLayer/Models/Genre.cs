using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Genre
{
    public int GenreId { get; set; }
    
    [Required]
    [MinLength(3)]
    public string GenreName { get; set; }
    
    // Navigation property
    public ICollection<VinylGenre> VinylGenres { get; set; }
}