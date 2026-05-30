using System.ComponentModel.DataAnnotations;

namespace OtoparkApp.Models
{
    public class TarifeDto
    {
        [Required(ErrorMessage = "Araç tipi zorunludur.")]
        [StringLength(50)]
        public string AracTipi { get; set; } = "";

        [Required]
        [Range(0, 9999, ErrorMessage = "Geçerli bir ücret girin.")]
        public decimal IlkSaatUcret { get; set; }

        [Required]
        [Range(0, 9999, ErrorMessage = "Geçerli bir ücret girin.")]
        public decimal OrtaSaatUcret { get; set; }

        [Required]
        [Range(0, 9999, ErrorMessage = "Geçerli bir ücret girin.")]
        public decimal UzunSaatUcret { get; set; }
    }
}
