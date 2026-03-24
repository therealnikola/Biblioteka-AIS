using Biblioteka.DAL.Data;
using Biblioteka.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.DAL.Repositories
{
    public class ZahtevRepository : IZahtevRepository
    {
        private readonly BibliotekaContext _context;

        public ZahtevRepository(BibliotekaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ZahtevZaPozajmljivanje>> GetAllAsync()
            => await _context.ZahteviZaPozajmljivanje
                .Include(z => z.Knjiga)
                .Include(z => z.Korisnik)
                .OrderByDescending(z => z.DatumZahteva)
                .ToListAsync();

        public async Task<IEnumerable<ZahtevZaPozajmljivanje>> GetByKorisnikAsync(string korisnikId)
            => await _context.ZahteviZaPozajmljivanje
                .Include(z => z.Knjiga)
                .Where(z => z.KorisnikId == korisnikId)
                .OrderByDescending(z => z.DatumZahteva)
                .ToListAsync();

        public async Task<ZahtevZaPozajmljivanje?> GetByIdAsync(int id)
            => await _context.ZahteviZaPozajmljivanje
                .Include(z => z.Knjiga)
                .Include(z => z.Korisnik)
                .FirstOrDefaultAsync(z => z.Id == id);

        public async Task AddAsync(ZahtevZaPozajmljivanje zahtev)
            => await _context.ZahteviZaPozajmljivanje.AddAsync(zahtev);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}