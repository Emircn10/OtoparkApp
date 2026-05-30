using OtoparkApp.Models;
using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OtoparkApp.Pages.GirisKayitlari
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GirisKayitDto GirisKayitDto { get; set; } = new();

        public List<SelectListItem> AracListesi { get; set; } = new();

        public void OnGet()
        {
            AracListesi = _context.Araclar
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Plaka + " - " + a.AracTipi
                }).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                AracListesi = _context.Araclar
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Plaka + " - " + a.AracTipi
                    }).ToList();
                return Page();
            }

            var kayit = new GirisKayit
            {
                AracId = GirisKayitDto.AracId,
                GirisTarihi = DateTime.Now,
                OdemeDurumu = "Bekliyor"
            };

            _context.GirisKayitlari.Add(kayit);
            _context.SaveChanges();

            return RedirectToPage("/GirisKayitlari/Index");
        }
    }
}
