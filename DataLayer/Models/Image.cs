using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Image
{
    public int ImageId { get; set; }
    [Required]
    public string Path { get; set; }
    
    // Navigation properties
    [Required]
    public int ProductId { get; set; }  // Foreign-Key
    public Product Product { get; set; }
}