using Microsoft.EntityFrameworkCore;
using OtoparkApp.Models;

namespace OtoparkApp.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Arac> Araclar { get; set; } = null!;
        public DbSet<GirisKayit> GirisKayitlari { get; set; } = null!;
        public DbSet<Tarife> Tarifeler { get; set; } = null!;
    }
}
