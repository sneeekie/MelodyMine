using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Order
{
    public int OrderId { get; set; }
    [StringLength(255)]
    public string Email { get; set; }

    public DateTime BuyDate { get; set; }
}