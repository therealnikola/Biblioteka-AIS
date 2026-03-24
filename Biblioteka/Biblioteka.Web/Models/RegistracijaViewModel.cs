using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Web.Models
{
    public class RegistracijaViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Lozinka mora imati najmanje 6 karaktera.")]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; } = string.Empty;

        [Required]
        [Compare("Lozinka", ErrorMessage = "Lozinke se ne poklapaju.")]
        [Display(Name = "Potvrdi lozinku")]
        [DataType(DataType.Password)]
        public string PotvrdaLozinke { get; set; } = string.Empty;
    }
}