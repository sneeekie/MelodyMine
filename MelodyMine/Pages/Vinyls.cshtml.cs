using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataLayer.Models;
using DataLayer.Services;
using System.Collections.Generic;
using System.Linq;

namespace MelodyMine.Pages
{
    public class VinylsModel : PageModel
    {
        private readonly IVinylService _vinylService;

        // Liste over vinylplader til visning på siden
        public IList<Vinyl> Vinyls { get; private set; }

        // Property til at binde input fra formular for ny vinyl
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

        // Metode til at håndtere POST-anmodninger for at tilføje ny vinyl
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Hvis modellen ikke er gyldig, returner den samme side for at vise fejl
                Vinyls = _vinylService.GetAllVinyls().ToList(); // Genindlæs liste over vinylplader
                return Page();
            }

            // Brug 'CreateVinyl' metode til at tilføje den nye vinyl
            _vinylService.CreateVinyl(NewVinyl);

            // Efter tilføjelse, redirect til Vinyls siden for at se den opdaterede liste
            return RedirectToPage();
        }
    }
}