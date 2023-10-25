namespace DataLayer.Models;

public class OrderProductDetails
{
    public int OrderProductDetailsId { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}