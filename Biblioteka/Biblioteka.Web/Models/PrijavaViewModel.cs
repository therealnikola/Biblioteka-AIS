using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Web.Models
{
    public class PrijavaViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; } = string.Empty;

        [Display(Name = "Zapamti me")]
        public bool ZapamtiMe { get; set; }
    }
}