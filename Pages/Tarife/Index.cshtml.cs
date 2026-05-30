using OtoparkApp.Models;
using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OtoparkApp.Pages.Tarife
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public List<OtoparkApp.Models.Tarife> tarifeList { get; set; } = new();

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            tarifeList = context.Tarifeler.OrderBy(t => t.AracTipi).ToList();
        }
    }
}
