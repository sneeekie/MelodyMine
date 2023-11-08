using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Services;
using DataLayer.Models;

namespace MelodyMine.Pages;

public class ShopModel : PageModel
{
    private readonly IVinylService _vinylService;

    public ShopModel(IVinylService vinylService)
    {
        _vinylService = vinylService;
    }

    public IQueryable<Vinyl> Vinyls { get; set; }
    
    public void OnGet()
    {
        Vinyls = _vinylService.GetAllVinyls();
    }
}