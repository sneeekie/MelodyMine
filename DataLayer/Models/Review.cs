namespace DataLayer.Models;

public class Review
{
    public int ReviewId { get; set; }
    public int NumStars { get; set; }
    public string? ReviewComment { get; set; }
}