using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Order
{
    public int OrderId { get; set; }
    [Required]
    [StringLength(255)]
    public string Email { get; set; }
    public DateTime BuyDate { get; set; }
    
    // Navigation properties
    public ICollection<OrderProductDetails> OrderProductDetails { get; set; }
}