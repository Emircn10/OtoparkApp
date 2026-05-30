using OtoparkApp.Models;
using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace OtoparkApp.Pages.GirisKayitlari
{
    public class CikisModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CikisModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public GirisKayit? Kayit { get; set; }

        [BindProperty]
        public CikisDto CikisDto { get; set; } = new();

        // Hesaplanan süre ve ücret bilgisi (gösterim için)
        public int ToplamDakika { get; set; }
        public decimal HesaplananUcret { get; set; }

        public IActionResult OnGet(int id)
        {
            Kayit = _context.GirisKayitlari
                .Include(g => g.Arac)
                .FirstOrDefault(g => g.Id == id);

            if (Kayit == null || Kayit.CikisTarihi != null)
                return RedirectToPage("/GirisKayitlari/Index");

            ToplamDakika = (int)(DateTime.Now - Kayit.GirisTarihi).TotalMinutes;
            HesaplananUcret = HesaplaUcret(Kayit.Arac?.AracTipi ?? "Otomobil", ToplamDakika);

            CikisDto.KayitId = id;
            CikisDto.OdenenUcret = HesaplananUcret;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var kayit = _context.GirisKayitlari
                .Include(g => g.Arac)
                .FirstOrDefault(g => g.Id == CikisDto.KayitId);

            if (kayit == null) return RedirectToPage("/GirisKayitlari/Index");

            kayit.CikisTarihi = DateTime.Now;
            kayit.ToplamSureDakika = (int)(kayit.CikisTarihi.Value - kayit.GirisTarihi).TotalMinutes;
            kayit.OdenenUcret = CikisDto.OdenenUcret;
            kayit.OdemeDurumu = "Ödendi";

            _context.SaveChanges();

            return RedirectToPage("/GirisKayitlari/Index");
        }

        // Tarifeden ücret hesapla
        private decimal HesaplaUcret(string aracTipi, int dakika)
        {
            var tarife = _context.Tarifeler.FirstOrDefault(t => t.AracTipi == aracTipi);
            if (tarife == null) return 0;

            double saat = dakika / 60.0;

            if (saat <= 1)
                return tarife.IlkSaatUcret;
            else if (saat <= 3)
                return tarife.IlkSaatUcret + (decimal)(saat - 1) * tarife.OrtaSaatUcret;
            else
                return tarife.IlkSaatUcret + 2 * tarife.OrtaSaatUcret + (decimal)(saat - 3) * tarife.UzunSaatUcret;
        }
    }
}
