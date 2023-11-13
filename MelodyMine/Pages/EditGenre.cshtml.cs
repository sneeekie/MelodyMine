using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using DataLayer.Services;

namespace MelodyMine.Pages
{
    public class EditGenreModel : PageModel
    {
        private readonly IGenreService _genreService;

        public EditGenreModel(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [BindProperty]
        public GenreUpdateModel UpdateModel { get; set; }


        public IActionResult OnGet(int id)
        {
            var genre = _genreService.GetGenresById(id).FirstOrDefault();
            UpdateModel = new GenreUpdateModel
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName
            };
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var genreToUpdate = new Genre
            {
                GenreId = UpdateModel.GenreId,
                GenreName = UpdateModel.GenreName
            };

            _genreService.UpdateGenre(genreToUpdate);
            return RedirectToPage("./Genres");
        }


    }
}