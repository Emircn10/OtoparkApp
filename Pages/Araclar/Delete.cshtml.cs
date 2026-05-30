using OtoparkApp.Models;
using OtoparkApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OtoparkApp.Pages.Araclar
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public DeleteModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Arac? Arac { get; set; }

        public IActionResult OnGet(int id)
        {
            Arac = context.Araclar.Find(id);
            if (Arac == null) return RedirectToPage("/Araclar/Index");
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var arac = context.Araclar.Find(id);
            if (arac != null)
            {
                context.Araclar.Remove(arac);
                context.SaveChanges();
            }
            return RedirectToPage("/Araclar/Index");
        }
    }
}
