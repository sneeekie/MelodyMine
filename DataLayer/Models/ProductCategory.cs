namespace DataLayer.Models;

public class ProductCategory
{
    public int ProductId { get; set; }  // Primary-Key
    public int CategoryId { get; set; } // Primary-Key
    
    // Navigation properties
    public Product Product { get; set; }
    public Category Category { get; set; }
}