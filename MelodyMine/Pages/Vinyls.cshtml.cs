using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using DataLayer.Services;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MelodyMine.Pages
{
    public class VinylsModel : PageModel
    {
        private readonly IVinylService _vinylService;

        public IList<Vinyl> Vinyls { get; private set; }
        
        [BindProperty]
        public Vinyl NewVinyl { get; set; }

        public VinylsModel(IVinylService vinylService)
        {
            _vinylService = vinylService;
        }

        public void OnGet()
        {
            Vinyls = _vinylService.GetAllVinyls().ToList();
        }
        
        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                Vinyls = _vinylService.GetAllVinyls().ToList();
                return Page();
            }
    
            try
            {
                _vinylService.CreateVinyl(NewVinyl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "An error occurred while creating the vinyl.");
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
            if (vinylToUpdate == null)
            {
                return NotFound();
            }

            return RedirectToPage();
        }

    }
}