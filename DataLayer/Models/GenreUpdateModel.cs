using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class GenreUpdateModel
{
    public int GenreId { get; set; }
    
    [Required(ErrorMessage = "Genre name is required.")]
    public string GenreName { get; set; }
}