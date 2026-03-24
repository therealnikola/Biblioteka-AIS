using Biblioteka.BLL.Services;
using Biblioteka.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Web.Controllers
{
    public class KnjigeController : Controller
    {
        private readonly IKnjigaService _knjigaService;
        private readonly IZahtevService _zahtevService;
        private readonly UserManager<IdentityUser> _userManager;

        public KnjigeController(IKnjigaService knjigaService,
            IZahtevService zahtevService,
            UserManager<IdentityUser> userManager)
        {
            _knjigaService = knjigaService;
            _zahtevService = zahtevService;
            _userManager = userManager;
        }

        // Svi mogu da vide listu
        public async Task<IActionResult> Index(string pretraga)
        {
            var knjige = await _knjigaService.GetSveKnjigeAsync(pretraga);
            ViewData["Pretraga"] = pretraga;
            return View(knjige);
        }

        public async Task<IActionResult> Detalji(int id)
        {
            var knjiga = await _knjigaService.GetKnjigaByIdAsync(id);
            if (knjiga == null) return NotFound();
            return View(knjiga);
        }

        // Samo Admin
        [Authorize(Roles = "Admin")]
        public IActionResult Dodaj() => View();

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dodaj(Knjiga knjiga)
        {
            if (!ModelState.IsValid) return View(knjiga);
            await _knjigaService.DodajKnjiguAsync(knjiga);
            TempData["Poruka"] = "Knjiga je uspešno dodata!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Obrisi(int id)
        {
            await _knjigaService.ObrisiKnjiguAsync(id);
            TempData["Poruka"] = "Knjiga je obrisana.";
            return RedirectToAction(nameof(Index));
        }

        // Samo Klijent — slanje zahteva
        [Authorize(Roles = "Klijent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PosaljiZahtev(int knjigaId)
        {
            var korisnik = await _userManager.GetUserAsync(User);
            if (korisnik == null) return Unauthorized();

            try
            {
                await _zahtevService.PosaljiZahtevAsync(knjigaId, korisnik.Id);
                TempData["Poruka"] = "Zahtev za pozajmljivanje je poslat!";
            }
            catch (InvalidOperationException ex)
            {
                TempData["Greška"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}