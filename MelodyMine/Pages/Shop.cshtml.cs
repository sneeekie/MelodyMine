using DataLayer.Models;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ShopModel : PageModel
{
    private readonly IVinylService _vinylService;
    private readonly IGenreService _genreService; // Tilføj en reference til genre service
    private const int PageSize = 9; // Dette kan også være en konfigurerbar værdi

    // Properties for at binde data til Razor Page
    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; }
    [BindProperty(SupportsGet = true)]
    public int? GenreId { get; set; }
    [BindProperty(SupportsGet = true)]
    public string TitleSort { get; set; }
    [BindProperty(SupportsGet = true)]
    public string PriceSort { get; set; }
    public int CurrentPage { get; set; } = 1;
    public PaginatedResult<Vinyl> PaginatedVinyls { get; set; }
    public IQueryable<Genre> Genres { get; set; } // Tilføj en property for Genres

    public ShopModel(IVinylService vinylService, IGenreService genreService) // Inkluder IGenreService i konstruktøren
    {
        _vinylService = vinylService;
        _genreService = genreService;
    }

    public void OnGet(int currentPage)
    {
        CurrentPage = currentPage <= 0 ? 1 : currentPage;
        Genres = _genreService.GetAllGenres(); // Hent alle genrer
        PaginatedVinyls = _vinylService.GetPaginatedVinyls(CurrentPage, PageSize, SearchTerm, GenreId, TitleSort, PriceSort);
    }
}