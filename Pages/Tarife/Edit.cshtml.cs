using OtoparkApp.Models;
using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OtoparkApp.Pages.Tarife
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TarifeDto TarifeDto { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var tarife = _context.Tarifeler.Find(id);
            if (tarife == null) return RedirectToPage("/Tarife/Index");

            TarifeDto = new TarifeDto
            {
                AracTipi = tarife.AracTipi,
                IlkSaatUcret = tarife.IlkSaatUcret,
                OrtaSaatUcret = tarife.OrtaSaatUcret,
                UzunSaatUcret = tarife.UzunSaatUcret
            };

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid) return Page();

            var tarife = _context.Tarifeler.Find(id);
            if (tarife == null) return RedirectToPage("/Tarife/Index");

            tarife.AracTipi = TarifeDto.AracTipi;
            tarife.IlkSaatUcret = TarifeDto.IlkSaatUcret;
            tarife.OrtaSaatUcret = TarifeDto.OrtaSaatUcret;
            tarife.UzunSaatUcret = TarifeDto.UzunSaatUcret;

            _context.SaveChanges();

            return RedirectToPage("/Tarife/Index");
        }
    }
}
