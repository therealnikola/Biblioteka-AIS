using Biblioteka.DAL.Data;
using Biblioteka.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.DAL.Repositories
{
    public class KnjigaRepository : IKnjigaRepository
    {
        private readonly BibliotekaContext _context;

        public KnjigaRepository(BibliotekaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Knjiga>> GetAllAsync(string? pretraga = null)
        {
            var query = _context.Knjige.AsQueryable();
            if (!string.IsNullOrEmpty(pretraga))
                query = query.Where(k =>
                    k.Naslov.Contains(pretraga) ||
                    k.Autor.Contains(pretraga));
            return await query.ToListAsync();
        }

        public async Task<Knjiga?> GetByIdAsync(int id)
            => await _context.Knjige.FindAsync(id);

        public async Task AddAsync(Knjiga knjiga)
            => await _context.Knjige.AddAsync(knjiga);

        public async Task DeleteAsync(int id)
        {
            var knjiga = await _context.Knjige.FindAsync(id);
            if (knjiga != null) _context.Knjige.Remove(knjiga);
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}