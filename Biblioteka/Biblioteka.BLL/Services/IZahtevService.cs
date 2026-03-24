using Biblioteka.DAL.Models;

namespace Biblioteka.BLL.Services
{
    public interface IZahtevService
    {
        Task<IEnumerable<ZahtevZaPozajmljivanje>> GetSviZahteviAsync();
        Task<IEnumerable<ZahtevZaPozajmljivanje>> GetZahteviKorisnikaAsync(string korisnikId);
        Task<ZahtevZaPozajmljivanje?> GetZahtevByIdAsync(int id);
        Task PosaljiZahtevAsync(int knjigaId, string korisnikId);
        Task ObradiZahtevAsync(int zahtevId, StatusZahteva status, string? napomena);
    }
}