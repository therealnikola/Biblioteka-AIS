using Biblioteka.DAL.Models;
using Biblioteka.DAL.Repositories;

namespace Biblioteka.BLL.Services
{
    public class ZahtevService : IZahtevService
    {
        private readonly IZahtevRepository _zahtevRepo;
        private readonly IKnjigaRepository _knjigaRepo;

        public ZahtevService(IZahtevRepository zahtevRepo, IKnjigaRepository knjigaRepo)
        {
            _zahtevRepo = zahtevRepo;
            _knjigaRepo = knjigaRepo;
        }

        public Task<IEnumerable<ZahtevZaPozajmljivanje>> GetSviZahteviAsync()
            => _zahtevRepo.GetAllAsync();

        public Task<IEnumerable<ZahtevZaPozajmljivanje>> GetZahteviKorisnikaAsync(string korisnikId)
            => _zahtevRepo.GetByKorisnikAsync(korisnikId);

        public Task<ZahtevZaPozajmljivanje?> GetZahtevByIdAsync(int id)
            => _zahtevRepo.GetByIdAsync(id);

        public async Task PosaljiZahtevAsync(int knjigaId, string korisnikId)
        {
            var knjiga = await _knjigaRepo.GetByIdAsync(knjigaId);
            if (knjiga == null || !knjiga.Dostupna)
                throw new InvalidOperationException("Knjiga nije dostupna za pozajmljivanje.");

            var zahtev = new ZahtevZaPozajmljivanje
            {
                KnjigaId = knjigaId,
                KorisnikId = korisnikId,
                DatumZahteva = DateTime.Now,
                Status = StatusZahteva.NaCekanju
            };

            await _zahtevRepo.AddAsync(zahtev);
            await _zahtevRepo.SaveAsync();
        }

        public async Task ObradiZahtevAsync(int zahtevId, StatusZahteva status, string? napomena)
        {
            var zahtev = await _zahtevRepo.GetByIdAsync(zahtevId);
            if (zahtev == null) return;

            zahtev.Status = status;
            zahtev.NapomenaAdmina = napomena;

            // Ako je odobren, označi knjigu kao nedostupnu
            if (status == StatusZahteva.Odobren)
            {
                var knjiga = await _knjigaRepo.GetByIdAsync(zahtev.KnjigaId);
                if (knjiga != null)
                {
                    knjiga.Dostupna = false;
                    await _knjigaRepo.SaveAsync();
                }
            }
            // Ako je vraćena, označi kao dostupnu
            else if (status == StatusZahteva.Vratio)
            {
                var knjiga = await _knjigaRepo.GetByIdAsync(zahtev.KnjigaId);
                if (knjiga != null)
                {
                    knjiga.Dostupna = true;
                    await _knjigaRepo.SaveAsync();
                }
            }

            await _zahtevRepo.SaveAsync();
        }
    }
}