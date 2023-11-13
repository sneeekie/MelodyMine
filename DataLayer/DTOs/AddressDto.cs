namespace DataLayer.DTOs;

public class AddressDto
{
    public int AddressId { get; set; }
    public int? Postal { get; set; }
    public int? StreetNumber { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Street { get; set; }
    public string CardNumber { get; set; }
}