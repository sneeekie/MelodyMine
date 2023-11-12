using DataLayer.Models;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MelodyMine.Pages;

public class ShopModel : PageModel
{
    private readonly IVinylService _vinylService;
    private readonly IGenreService _genreService;
    private const int PageSize = 9;
    
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
    public IQueryable<Genre> Genres { get; set; }

    public ShopModel(IVinylService vinylService, IGenreService genreService)
    {
        _vinylService = vinylService;
        _genreService = genreService;
    }

    public void OnGet(int currentPage)
    {
        CurrentPage = currentPage <= 0 ? 1 : currentPage;
        Genres = _genreService.GetAllGenres(); 
        PaginatedVinyls = _vinylService.GetPaginatedVinyls(CurrentPage, PageSize, SearchTerm, GenreId, TitleSort, PriceSort);
    }
    
    public IActionResult OnPostAddToCart(int vinylId, int quantity = 1)
    {
        var shoppingCartItems = new List<ShoppingCartItem>();
        
        string existingCart = HttpContext.Session.GetString("ShoppingCart");
        if (!string.IsNullOrEmpty(existingCart))
        {
            shoppingCartItems = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(existingCart);
        }
        
        var vinyl = _vinylService.GetVinylById(vinylId);
        var shoppingCartItem = shoppingCartItems.FirstOrDefault(item => item.VinylId == vinylId);
        if (shoppingCartItem != null)
        {
            shoppingCartItem.Quantity += quantity;
        }
        else
        {
            shoppingCartItems.Add(new ShoppingCartItem
            {
                VinylId = vinylId,
                Title = vinyl.Title,
                Price = vinyl.Price,
                Quantity = quantity
            });
        }
        HttpContext.Session.SetString("ShoppingCart", JsonConvert.SerializeObject(shoppingCartItems));

        return RedirectToPage();
    }
}