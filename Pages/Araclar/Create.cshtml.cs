using OtoparkApp.Models;
using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OtoparkApp.Pages.Araclar
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AracDto AracDto { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            Arac arac = new()
            {
                Plaka = AracDto.Plaka.ToUpper(),
                AracTipi = AracDto.AracTipi,
                SahibiAdi = AracDto.SahibiAdi,
                SahibiTelefon = AracDto.SahibiTelefon,
                OlusturmaTarihi = DateTime.Now
            };

            _context.Araclar.Add(arac);
            _context.SaveChanges();

            return RedirectToPage("/Araclar/Index");
        }
    }
}
