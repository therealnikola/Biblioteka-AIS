using Biblioteka.BLL.Services;
using Biblioteka.DAL.Data;
using Biblioteka.DAL.Models;
using Biblioteka.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Baza podataka
builder.Services.AddDbContext<BibliotekaContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("BibliotekaConnection")));

// ASP.NET Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<BibliotekaContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Nalog/Prijava";
    options.AccessDeniedPath = "/Nalog/Zabranjen";
});

// DAL — Repozitorijumi
builder.Services.AddScoped<IKnjigaRepository, KnjigaRepository>();
builder.Services.AddScoped<IZahtevRepository, ZahtevRepository>();

// BLL — Servisi
builder.Services.AddScoped<IKnjigaService, KnjigaService>();
builder.Services.AddScoped<IZahtevService, ZahtevService>();

var app = builder.Build();

// Kreiranje Admin uloge i admin korisnika pri pokretanju
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var db = scope.ServiceProvider.GetRequiredService<BibliotekaContext>();
    db.Database.Migrate();
    // ✔️ SEED KNJIGA
    if (!db.Knjige.Any())
    {
        db.Knjige.AddRange(
            new Knjiga { Naslov = "Prokleta avlija", Autor = "Ivo Andric", Godina = 1954, ISBN = "978-86-331-6789-6", Dostupna = true },
            new Knjiga { Naslov = "Tvrdjava", Autor = "Mesa Selimovic", Godina = 1970, ISBN = "978-86-331-7890-7", Dostupna = true },
            new Knjiga { Naslov = "Seobe", Autor = "Milos Crnjanski", Godina = 1929, ISBN = "978-86-331-8901-8", Dostupna = false },
            new Knjiga { Naslov = "Druga knjiga Seoba", Autor = "Milos Crnjanski", Godina = 1962, ISBN = "978-86-331-9012-9", Dostupna = true },
            new Knjiga { Naslov = "Koreni", Autor = "Dobrica Cosic", Godina = 1954, ISBN = "978-86-331-1122-3", Dostupna = true },
            new Knjiga { Naslov = "Vreme smrti", Autor = "Dobrica Cosic", Godina = 1972, ISBN = "978-86-331-2233-4", Dostupna = false },
            new Knjiga { Naslov = "Zona Zamfirova", Autor = "Stevan Sremac", Godina = 1906, ISBN = "978-86-331-3344-5", Dostupna = true },
            new Knjiga { Naslov = "Pop Cira i pop Spira", Autor = "Stevan Sremac", Godina = 1894, ISBN = "978-86-331-4455-6", Dostupna = true },
            new Knjiga { Naslov = "Orlovi rano lete", Autor = "Branko Copic", Godina = 1957, ISBN = "978-86-331-5566-7", Dostupna = true },
            new Knjiga { Naslov = "Magarece godine", Autor = "Branko Copic", Godina = 1960, ISBN = "978-86-331-6677-8", Dostupna = false },
            new Knjiga { Naslov = "Zlocin i kazna", Autor = "Fjodor Dostojevski", Godina = 1866, ISBN = "978-86-331-7788-9", Dostupna = true },
            new Knjiga { Naslov = "Idioti", Autor = "Fjodor Dostojevski", Godina = 1869, ISBN = "978-86-331-8899-0", Dostupna = true },
            new Knjiga { Naslov = "Ana Karenjina", Autor = "Lav Tolstoj", Godina = 1877, ISBN = "978-86-331-9900-1", Dostupna = true },
            new Knjiga { Naslov = "Rat i mir", Autor = "Lav Tolstoj", Godina = 1869, ISBN = "978-86-331-1010-2", Dostupna = false },
            new Knjiga { Naslov = "1984", Autor = "George Orwell", Godina = 1949, ISBN = "978-86-331-2020-3", Dostupna = true },
            new Knjiga { Naslov = "Zivotinjska farma", Autor = "George Orwell", Godina = 1945, ISBN = "978-86-331-3030-4", Dostupna = true },
            new Knjiga { Naslov = "Lovac u zitu", Autor = "J.D. Salinger", Godina = 1951, ISBN = "978-86-331-4040-5", Dostupna = true },
            new Knjiga { Naslov = "Veliki Getsbi", Autor = "F. Scott Fitzgerald", Godina = 1925, ISBN = "978-86-331-5050-6", Dostupna = false },
            new Knjiga { Naslov = "Mali princ", Autor = "Antoan de Sent Egziperi", Godina = 1943, ISBN = "978-86-331-6060-7", Dostupna = true },
            new Knjiga { Naslov = "Alhemicar", Autor = "Paulo Koeljo", Godina = 1988, ISBN = "978-86-331-7070-8", Dostupna = true }
        );

        db.SaveChanges();
    }

    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    if (!await roleManager.RoleExistsAsync("Klijent"))
        await roleManager.CreateAsync(new IdentityRole("Klijent"));

    // Kreiranje admin korisnika
    var adminEmail = "admin@biblioteka.rs";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        await userManager.CreateAsync(admin, "Admin123");
        await userManager.AddToRoleAsync(admin, "Admin");
    }
}



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Knjige}/{action=Index}/{id?}");

app.Run();