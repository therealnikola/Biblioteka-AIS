using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Biblioteka.DAL.Models
{
    public enum StatusZahteva
    {
        NaCekanju,
        Odobren,
        Odbijen,
        Vratio
    }

    public class ZahtevZaPozajmljivanje
    {
        public int Id { get; set; }

        [Required]
        public int KnjigaId { get; set; }
        public Knjiga Knjiga { get; set; } = null!;

        [Required]
        public string KorisnikId { get; set; } = string.Empty;
        public IdentityUser Korisnik { get; set; } = null!;

        [Display(Name = "Datum zahteva")]
        public DateTime DatumZahteva { get; set; } = DateTime.Now;

        [Display(Name = "Status")]
        public StatusZahteva Status { get; set; } = StatusZahteva.NaCekanju;

        [Display(Name = "Napomena admina")]
        public string? NapomenaAdmina { get; set; }
    }
}