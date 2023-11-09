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
    
    
    /*
     * Visa: 13 or 16 digits, starting with 4.
     * MasterCard: 16 digits, starting with 51-55 or 2221-2720.
     * American Express: 15 digits, starting with 34 or 37.   
     */
    [Required(ErrorMessage = "Card number is required.")]
    [CreditCard(ErrorMessage = "Invalid card number.")]
    public string CardNumber { get; set; }
}