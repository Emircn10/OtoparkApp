using System.ComponentModel.DataAnnotations;

namespace OtoparkApp.Models
{
    public class GirisKayitDto
    {
        [Required(ErrorMessage = "Araç seçimi zorunludur.")]
        public int AracId { get; set; }

        [Required(ErrorMessage = "Giriş tarihi zorunludur.")]
        public DateTime GirisTarihi { get; set; } = DateTime.Now;
    }

    public class CikisDto
    {
        [Required]
        public int KayitId { get; set; }

        [Required(ErrorMessage = "Ödenen ücret girilmelidir.")]
        [Range(0, 99999, ErrorMessage = "Geçerli bir ücret girin.")]
        public decimal OdenenUcret { get; set; }
    }
}
