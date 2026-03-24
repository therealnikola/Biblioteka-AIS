using System.ComponentModel.DataAnnotations;

namespace Biblioteka.DAL.Models
{
    public class Knjiga
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naslov je obavezan.")]
        [Display(Name = "Naslov")]
        public string Naslov { get; set; } = string.Empty;

        [Required(ErrorMessage = "Autor je obavezan.")]
        [Display(Name = "Autor")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Godina je obavezna.")]
        [Range(1000, 2100, ErrorMessage = "Unesite validnu godinu.")]
        [Display(Name = "Godina izdanja")]
        public int Godina { get; set; }

        [Display(Name = "ISBN")]
        public string? ISBN { get; set; }

        [Display(Name = "Dostupna")]
        public bool Dostupna { get; set; } = true;

        // Navigaciono svojstvo
        public ICollection<ZahtevZaPozajmljivanje> Zahtevi { get; set; } = new List<ZahtevZaPozajmljivanje>();
    }
}