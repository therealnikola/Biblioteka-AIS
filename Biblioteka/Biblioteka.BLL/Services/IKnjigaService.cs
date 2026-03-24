using Biblioteka.DAL.Models;

namespace Biblioteka.BLL.Services
{
    public interface IKnjigaService
    {
        Task<IEnumerable<Knjiga>> GetSveKnjigeAsync(string? pretraga = null);
        Task<Knjiga?> GetKnjigaByIdAsync(int id);
        Task DodajKnjiguAsync(Knjiga knjiga);
        Task ObrisiKnjiguAsync(int id);
    }
}