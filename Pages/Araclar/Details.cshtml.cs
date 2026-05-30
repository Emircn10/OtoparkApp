using OtoparkApp.Models;
using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace OtoparkApp.Pages.Araclar
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public DetailsModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Arac? Arac { get; set; }
        public List<GirisKayit> GirisKayitlari { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            Arac = context.Araclar.Find(id);
            if (Arac == null) return RedirectToPage("/Araclar/Index");

            GirisKayitlari = context.GirisKayitlari
                .Where(g => g.AracId == id)
                .OrderByDescending(g => g.GirisTarihi)
                .ToList();

            return Page();
        }
    }
}
