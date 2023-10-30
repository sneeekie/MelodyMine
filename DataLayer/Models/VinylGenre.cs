namespace DataLayer.Models;

public class VinylGenre
{
    public int VinylId { get; set; }  // Primary-Key
    public int GenreId { get; set; }  // Primary-Key
    
    // Navigation properties
    public Vinyl Vinyl { get; set; }
    public Genre Genre { get; set; }
}