using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Address
{
    public int AddressId { get; set; }
    
    [Required(ErrorMessage = "Postal code is required.")]
    public int? Postal { get; set; }
    
    [Required(ErrorMessage = "Street number is required.")]
    public int? StreetNumber { get; set; }
    
    [Required(ErrorMessage = "City is required.")]
    public string City { get; set; }
    
    [Required(ErrorMessage = "Country is required.")]
    public string Country { get; set; }
    
    [Required(ErrorMessage = "Street is required.")]
    public string Street { get; set; }
    
    [Required(ErrorMessage = "Card number is required.")]
    public string CardNumber { get; set; }
}