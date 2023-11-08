using System.ComponentModel.DataAnnotations;
using DataLayer.Models;

public class Order
{
    public int OrderId { get; set; }
    [Required]
    [StringLength(255)]
    public string Email { get; set; }
    [Required]
    public DateTime BuyDate { get; set; }
    
    [Required]
    public int AddressId { get; set; }

    // Navigation properties
    public ICollection<OrderProductDetails> OrderProductDetails { get; set; }
    public Address Address { get; set; }
}