using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MelodyMine.Pages;

public class EditVinylModel : PageModel
{
    private readonly IVinylService _vinylService;
    private readonly IGenreService _genreService;
    
    [BindProperty]
    public Vinyl UpdateModel { get; set; }
    public SelectList GenreOptions { get; private set; }

    public EditVinylModel(IVinylService vinylService, IGenreService genreService)
    {
        _vinylService = vinylService;
        _genreService = genreService;
    }
    
    public void OnGet(int id)
    {
        var vinyl = _vinylService.GetVinylById(id);
        if (vinyl == null)
        {
            RedirectToPage("./NotFound");
            return;
        }

        UpdateModel = vinyl;
        GenreOptions = new SelectList(_genreService.GetAllGenres(), "GenreId", "GenreName", UpdateModel.GenreId);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            GenreOptions = new SelectList(_genreService.GetAllGenres(), "GenreId", "GenreName", UpdateModel.GenreId);
            return Page();
        }

        try
        {
            _vinylService.UpdateVinylBy(UpdateModel.VinylId, UpdateModel);
            
            if (UpdateModel.GenreId.HasValue)
            {
                _genreService.UpdateVinylGenreLink(UpdateModel.VinylId, UpdateModel.GenreId.Value);
            }

            return RedirectToPage("./Vinyls");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Der opstod en fejl under opdatering af vinyl eller genre: {ex.Message}");
            GenreOptions = new SelectList(_genreService.GetAllGenres(), "GenreId", "GenreName", UpdateModel.GenreId);
            return Page();
        }
    }


}