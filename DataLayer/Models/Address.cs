using System.ComponentModel.DataAnnotations;
using DataLayer.Models;

public class Address
{
    public int AddressId { get; set; }
    [Required]
    public int Postal { get; set; }
    [Required]
    public int StreetNumber { get; set; }
    [Required]
    [StringLength(100)]
    public string City { get; set; }
    [Required]
    [StringLength(100)]
    public string Country { get; set; }
    [Required]
    [StringLength(100)]
    public string Street { get; set; }
    [Required]
    public long CardNumber { get; set; }
}