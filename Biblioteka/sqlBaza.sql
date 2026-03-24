-- Ako već imaš bazu Biblioteka, dropi je i napravi novu:
USE master;
GO
DROP DATABASE IF EXISTS Biblioteka;
GO
CREATE DATABASE Biblioteka;
GO
USE Biblioteka;
GO

-- ASP.NET Identity tabele se kreiraju automatski pri pokretanju.
-- Tabela knjiga:
CREATE TABLE Knjige (
    Id       INT IDENTITY(1,1) PRIMARY KEY,
    Naslov   NVARCHAR(200) NOT NULL,
    Autor    NVARCHAR(150) NOT NULL,
    Godina   INT NOT NULL,
    ISBN     NVARCHAR(20) NULL,
    Dostupna BIT NOT NULL DEFAULT 1
);
GO

-- Testne knjige:
INSERT INTO Knjige (Naslov, Autor, Godina, ISBN, Dostupna) VALUES
('Na Drini cuprija',   'Ivo Andric',        1945, '978-86-331-1234-1', 1),
('Dervid i smrt',      'Mesa Selimovic',    1966, '978-86-331-2345-2', 1),
('Noz',                'Vuk Draskovic',     1982, '978-86-331-3456-3', 0),
('Hazarski recnik',    'Milorad Pavic',     1984, '978-86-331-4567-4', 1),
('Gospodar prstenova', 'J.R.R. Tolkien',    1954, '978-86-331-5678-5', 1);
GO