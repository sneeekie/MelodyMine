namespace DataLayer.DTOs;

public class VinylGenreDto
{
    public int VinylId { get; set; }
    public int GenreId { get; set; }
    public GenreDto Genre { get; set; } 
}