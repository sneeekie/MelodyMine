using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class GenreUpdateModel
{
    public int GenreId { get; set; }
    [Required]
    public string GenreName { get; set; }
}