using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Services;
using System.Collections.Generic;
using System.Linq;
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

    // Add Genre
    public IActionResult OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _genreService.CreateGenre(new Genre { GenreName = NewGenreName });
        return RedirectToPage();
    }
    
    // Delete Genre
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        bool result = _genreService.DeleteGenre(id);
        if (!result)
        {
            return NotFound();
        }

        return RedirectToPage();
    }

}