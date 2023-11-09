using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Services;
using DataLayer.Models;
using System.Linq;

namespace MelodyMine.Pages;

public class ShopModel : PageModel
{
    private readonly IVinylService _vinylService;
    private readonly IGenreService _genreService;

    public ShopModel(IVinylService vinylService, IGenreService genreService)
    {
        _vinylService = vinylService;
        _genreService = genreService;
    }

    [BindProperty(SupportsGet = true)]
    public string TitleSort { get; set; }

    [BindProperty(SupportsGet = true)]
    public string PriceSort { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? GenreId { get; set; }

    public IQueryable<Vinyl> Vinyls { get; set; }
    public IQueryable<Genre> Genres { get; set; }
    
    public void OnGet(string titleSort, string priceSort)
    {
        Genres = _genreService.GetAllGenres();
        Vinyls = _vinylService.FilterVinyls(SearchTerm, GenreId, titleSort, priceSort);
    }

}