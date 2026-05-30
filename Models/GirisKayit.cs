using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtoparkApp.Models
{
    public class GirisKayit
    {
        public int Id { get; set; }

        public int AracId { get; set; }

        public DateTime GirisTarihi { get; set; } = DateTime.Now;

        public DateTime? CikisTarihi { get; set; }

        public int? ToplamSureDakika { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? OdenenUcret { get; set; }

        public string OdemeDurumu { get; set; } = "Bekliyor";

        // Navigation property
        public Arac? Arac { get; set; }
    }
}
