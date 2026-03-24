using Biblioteka.DAL.Models;

namespace Biblioteka.DAL.Repositories
{
    public interface IZahtevRepository
    {
        Task<IEnumerable<ZahtevZaPozajmljivanje>> GetAllAsync();
        Task<IEnumerable<ZahtevZaPozajmljivanje>> GetByKorisnikAsync(string korisnikId);
        Task<ZahtevZaPozajmljivanje?> GetByIdAsync(int id);
        Task AddAsync(ZahtevZaPozajmljivanje zahtev);
        Task SaveAsync();
    }
}