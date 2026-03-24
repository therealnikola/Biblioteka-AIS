using Biblioteka.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.DAL.Data
{
    public class BibliotekaContext : IdentityDbContext<IdentityUser>
    {
        public BibliotekaContext(DbContextOptions<BibliotekaContext> options)
            : base(options) { }

        public DbSet<Knjiga> Knjige { get; set; }
        public DbSet<ZahtevZaPozajmljivanje> ZahteviZaPozajmljivanje { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ZahtevZaPozajmljivanje>()
                .HasOne(z => z.Knjiga)
                .WithMany(k => k.Zahtevi)
                .HasForeignKey(z => z.KnjigaId);

            builder.Entity<ZahtevZaPozajmljivanje>()
                .HasOne(z => z.Korisnik)
                .WithMany()
                .HasForeignKey(z => z.KorisnikId);
        }
    }
}