using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Biblioteka.Web.Models;

namespace Biblioteka.Web.Controllers
{
    public class NalogController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public NalogController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Prijava() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Prijava(PrijavaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Lozinka, model.ZapamtiMe, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Knjige");

            ModelState.AddModelError("", "Pogrešan email ili lozinka.");
            return View(model);
        }

        public IActionResult Registracija() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registracija(RegistracijaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var korisnik = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(korisnik, model.Lozinka);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(korisnik, "Klijent");
                await _signInManager.SignInAsync(korisnik, false);
                return RedirectToAction("Index", "Knjige");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        public async Task<IActionResult> Odjava()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Prijava");
        }

        public IActionResult Zabranjen() => View();
    }
}