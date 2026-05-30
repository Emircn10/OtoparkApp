using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OtoparkApp.Pages.Tarife
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public OtoparkApp.Models.Tarife? Tarife { get; set; }

        public DeleteModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult OnGet(int id)
        {
            Tarife = context.Tarifeler.Find(id);
            if (Tarife == null) return RedirectToPage("/Tarife/Index");
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var tarife = context.Tarifeler.Find(id);
            if (tarife != null)
            {
                context.Tarifeler.Remove(tarife);
                context.SaveChanges();
            }
            return RedirectToPage("/Tarife/Index");
        }
    }
}
