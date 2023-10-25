namespace DataLayer.Models;

public class Address
{
    public int AddressId { get; set; }
    public int Postal { get; set; }
    public int StreetNumber { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Street { get; set; }
    
    // Navigation property
    public Manufacturer Manufacturer { get; set; }
}