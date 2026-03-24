using Biblioteka.BLL.Services;
using Biblioteka.DAL.Models;
using Biblioteka.DAL.Repositories;

namespace Biblioteka.BLL.Services
{
    public class KnjigaService : IKnjigaService
    {
        private readonly IKnjigaRepository _repo;

        public KnjigaService(IKnjigaRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Knjiga>> GetSveKnjigeAsync(string? pretraga = null)
            => _repo.GetAllAsync(pretraga);

        public Task<Knjiga?> GetKnjigaByIdAsync(int id)
            => _repo.GetByIdAsync(id);

        public async Task DodajKnjiguAsync(Knjiga knjiga)
        {
            await _repo.AddAsync(knjiga);
            await _repo.SaveAsync();
        }

        public async Task ObrisiKnjiguAsync(int id)
        {
            await _repo.DeleteAsync(id);
            await _repo.SaveAsync();
        }
    }
}