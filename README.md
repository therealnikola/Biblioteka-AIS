Web aplikacija za upravljanje bibliotekom 
razvijena korišćenjem ASP.NET Core MVC i .NET 8 tehnologija.
Omogućava pregled knjiga, slanje zahteva za pozajmljivanje i
administraciju bibliotečkog fonda.

---------

Preduslovi
.NET 8 SDK
SQL Server Express ili LocalDB
Visual Studio 2022 (preporučeno)

-------

 ADMIN KORISNIK

Admin :  username :admin@biblioteka.rs ||| Password: Admin123

-----------------------
Provera LocalDB instance:

sqllocaldb info MSSQLLocalDB

Ako ne postoji:

sqllocaldb create MSSQLLocalDB
sqllocaldb start MSSQLLocalDB

Pokretanje projekta
Kloniraj repozitorijum
Podesi connection string u appsettings.json
Pokreni migracije:
Add-Migration InitialCreate
Update-Database
Pokreni aplikaciju (F5)
