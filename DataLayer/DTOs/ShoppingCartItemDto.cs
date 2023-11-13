namespace DataLayer.DTOs;

public class ShoppingCartItemDto
{
    public int VinylId { get; set; }
    public int Quantity { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
}