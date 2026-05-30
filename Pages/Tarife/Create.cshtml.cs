using OtoparkApp.Models;
using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OtoparkApp.Pages.Tarife
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TarifeDto TarifeDto { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var tarife = new OtoparkApp.Models.Tarife
            {
                AracTipi = TarifeDto.AracTipi,
                IlkSaatUcret = TarifeDto.IlkSaatUcret,
                OrtaSaatUcret = TarifeDto.OrtaSaatUcret,
                UzunSaatUcret = TarifeDto.UzunSaatUcret
            };

            _context.Tarifeler.Add(tarife);
            _context.SaveChanges();

            return RedirectToPage("/Tarife/Index");
        }
    }
}
