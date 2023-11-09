using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using DataLayer.Services;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MelodyMine.Pages
{
    public class VinylsModel : PageModel
    {
        private readonly IVinylService _vinylService;
        private readonly IGenreService _genreService;

        public IList<Vinyl> Vinyls { get; private set; }
        
        [BindProperty]
        public Vinyl NewVinyl { get; set; }
        public SelectList GenreOptions { get; private set; }

        public VinylsModel(IVinylService vinylService, IGenreService genreService)
        {
            _vinylService = vinylService;
            _genreService = genreService;

        }

        public void OnGet()
        {
            Vinyls = _vinylService.GetAllVinyls().ToList();
            GenreOptions = new SelectList(_genreService.GetAllGenres(), "GenreId", "GenreName");

        }
        
        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                Vinyls = _vinylService.GetAllVinyls().ToList();
                GenreOptions = new SelectList(_genreService.GetAllGenres(), "GenreId", "GenreName"); // Genopfyld genre options
                return Page();
            }

            try
            {
                _vinylService.CreateVinyl(NewVinyl);
        
                if (NewVinyl.GenreId.HasValue)
                {
                    _genreService.CreateVinylGenre(NewVinyl.VinylId, NewVinyl.GenreId.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "An error occurred while creating the vinyl.");
                GenreOptions = new SelectList(_genreService.GetAllGenres(), "GenreId", "GenreName"); // Genopfyld genre options, hvis der sker en fejl
                return Page();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostUpdate(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var vinylToUpdate = _vinylService.GetVinylById(id);

            return RedirectToPage();
        }
        
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                _vinylService.DeleteVinylById(id);
                return RedirectToPage("./Vinyls");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}