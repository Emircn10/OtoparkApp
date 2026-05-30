using OtoparkApp.Models;
using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace OtoparkApp.Pages.GirisKayitlari
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public List<GirisKayit> kayitList { get; set; } = new();

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            kayitList = context.GirisKayitlari
                .Include(g => g.Arac)
                .OrderByDescending(g => g.GirisTarihi)
                .ToList();
        }
    }
}
