using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class RecordLabel
{
    public int RecordLabelId { get; set; } 
    [Required]
    public string LabelName { get; set; } 
    public int? PhoneNumber { get; set; }
    public string? Email { get; set; }
    
    // Navigation Properties
    public int? AddressId { get; set; } // Foreign-Key
    public Address Address { get; set; }
    public ICollection<Vinyl> Vinyls { get; set; }
}