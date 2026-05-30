using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OtoparkApp.Models
{
    public class Arac
    {
        public int Id { get; set; }

        public string Plaka { get; set; } = "";
        public string AracTipi { get; set; } = "";

        public string? SahibiAdi { get; set; }
        public string? SahibiTelefon { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Navigation property
        public ICollection<GirisKayit> GirisKayitlari { get; set; } = new List<GirisKayit>();
    }
}
