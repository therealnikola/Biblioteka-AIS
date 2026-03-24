using Biblioteka.DAL.Models;

namespace Biblioteka.DAL.Repositories
{
    public interface IKnjigaRepository
    {
        Task<IEnumerable<Knjiga>> GetAllAsync(string? pretraga = null);
        Task<Knjiga?> GetByIdAsync(int id);
        Task AddAsync(Knjiga knjiga);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}