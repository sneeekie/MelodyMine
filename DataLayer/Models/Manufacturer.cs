using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Manufacturer
{
    public int ManufacturerId { get; set; }
    [Required]
    public string ManufacturerName { get; set; }
    public int? PhoneNumber { get; set; }
    public string? Email { get; set; }
    
    // Navigation Properties
    public int? AddressId { get; set; } // Foreign-Key
    public Address Address { get; set; }
    public ICollection<Product> Products { get; set; }
}