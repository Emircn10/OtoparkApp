using System.ComponentModel.DataAnnotations;

namespace OtoparkApp.Models
{
    public class AracDto
    {
        [Required(ErrorMessage = "Plaka zorunludur.")]
        [StringLength(15, ErrorMessage = "Plaka en fazla 15 karakter olabilir.")]
        public string Plaka { get; set; } = "";

        [Required(ErrorMessage = "Araç tipi zorunludur.")]
        [StringLength(50)]
        public string AracTipi { get; set; } = "";

        [StringLength(100)]
        public string? SahibiAdi { get; set; }

        [StringLength(20)]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
        public string? SahibiTelefon { get; set; }
    }
}
