namespace DataLayer.DTOs;

public class VinylDto
{
    public int VinylId { get; set; }
    public string Title { get; set; }
    public string? Artist { get; set; }
    public decimal Price { get; set; }
    public string ImagePath { get; set; }
    public List<GenreDto> Genres { get; set; }
}