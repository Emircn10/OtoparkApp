using OtoparkApp.Models;
using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OtoparkApp.Pages.Araclar
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public List<Arac> aracList { get; set; } = new();

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            aracList = context.Araclar
                .OrderByDescending(a => a.OlusturmaTarihi)
                .ToList();
        }
    }
}
