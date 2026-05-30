using OtoparkApp.Models;
using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OtoparkApp.Pages.Araclar
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AracDto AracDto { get; set; } = new();

        public Arac? Arac { get; set; }

        public IActionResult OnGet(int id)
        {
            Arac = _context.Araclar.Find(id);
            if (Arac == null) return RedirectToPage("/Araclar/Index");

            AracDto = new AracDto
            {
                Plaka = Arac.Plaka,
                AracTipi = Arac.AracTipi,
                SahibiAdi = Arac.SahibiAdi,
                SahibiTelefon = Arac.SahibiTelefon
            };

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid) return Page();

            var arac = _context.Araclar.Find(id);
            if (arac == null) return RedirectToPage("/Araclar/Index");

            arac.Plaka = AracDto.Plaka.ToUpper();
            arac.AracTipi = AracDto.AracTipi;
            arac.SahibiAdi = AracDto.SahibiAdi;
            arac.SahibiTelefon = AracDto.SahibiTelefon;

            _context.SaveChanges();

            return RedirectToPage("/Araclar/Index");
        }
    }
}
