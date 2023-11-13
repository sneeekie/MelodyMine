using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Services;
using DataLayer.Models;

namespace MelodyMine.Pages;

public class GenresModel : PageModel
{
    private readonly IGenreService _genreService;

    public List<Genre> Genres { get; set; }

    [BindProperty]
    public string NewGenreName { get; set; }

    public GenresModel(IGenreService genreService)
    {
        _genreService = genreService;
    }

    public void OnGet()
    {
        Genres = _genreService.GetAllGenres().ToList();
    }
    
    public IActionResult OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Genres = _genreService.GetAllGenres().ToList();
            return Page();
        }

        _genreService.CreateGenre(new Genre { GenreName = NewGenreName });
        return RedirectToPage();
    }

    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        bool result = _genreService.DeleteGenre(id);
        
        return RedirectToPage();
    }

}