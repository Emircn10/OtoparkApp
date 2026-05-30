using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtoparkApp.Models
{
    public class Tarife
    {
        public int Id { get; set; }

        public string AracTipi { get; set; } = "";

        // 0-1 saat ücreti
        [Column(TypeName = "decimal(18,2)")]
        public decimal IlkSaatUcret { get; set; }

        // 1-3 saat arası saatlik ücret
        [Column(TypeName = "decimal(18,2)")]
        public decimal OrtaSaatUcret { get; set; }

        // 3+ saat sonrası saatlik ücret
        [Column(TypeName = "decimal(18,2)")]
        public decimal UzunSaatUcret { get; set; }
    }
}
