namespace DataLayer.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public string Email { get; set; }
    public DateTime BuyDate { get; set; }
    public AddressDto Address { get; set; }
}