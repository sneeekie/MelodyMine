using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Genre
{
    public int GenreId { get; set; }
    
    [Required]
    public string GenreName { get; set; }
    
    // Navigation property
    public ICollection<VinylGenre> Vinyls { get; set; }
}