using Biblioteka.BLL.Services;
using Biblioteka.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Web.Controllers
{
    public class ZahteviController : Controller
    {
        private readonly IZahtevService _zahtevService;
        private readonly UserManager<IdentityUser> _userManager;

        public ZahteviController(IZahtevService zahtevService,
            UserManager<IdentityUser> userManager)
        {
            _zahtevService = zahtevService;
            _userManager = userManager;
        }

        // Admin vidi sve zahteve
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var zahtevi = await _zahtevService.GetSviZahteviAsync();
            return View(zahtevi);
        }

        // Admin obrađuje zahtev
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detalji(int id)
        {
            var zahtev = await _zahtevService.GetZahtevByIdAsync(id);
            if (zahtev == null) return NotFound();
            return View(zahtev);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ObradiZahtev(int id, string akcija, string? napomena)
        {
            var status = akcija == "odobri" ? StatusZahteva.Odobren
                       : akcija == "vrati" ? StatusZahteva.Vratio
                       : StatusZahteva.Odbijen;

            await _zahtevService.ObradiZahtevAsync(id, status, napomena);
            TempData["Poruka"] = "Zahtev je obrađen.";
            return RedirectToAction(nameof(Index));
        }

        // Klijent vidi svoje zahteve
        [Authorize(Roles = "Klijent")]
        public async Task<IActionResult> MojiZahtevi()
        {
            var korisnik = await _userManager.GetUserAsync(User);
            if (korisnik == null) return Unauthorized();
            var zahtevi = await _zahtevService.GetZahteviKorisnikaAsync(korisnik.Id);
            return View(zahtevi);
        }
    }
}