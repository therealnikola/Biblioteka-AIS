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
('Prokleta avlija',        'Ivo Andric',         1954, '978-86-331-6789-6', 1),
('Tvrdjava',               'Mesa Selimovic',     1970, '978-86-331-7890-7', 1),
('Seobe',                  'Milos Crnjanski',    1929, '978-86-331-8901-8', 0),
('Druga knjiga Seoba',     'Milos Crnjanski',    1962, '978-86-331-9012-9', 1),
('Koreni',                 'Dobrica Cosic',      1954, '978-86-331-1122-3', 1),
('Vreme smrti',            'Dobrica Cosic',      1972, '978-86-331-2233-4', 0),
('Zona Zamfirova',         'Stevan Sremac',      1906, '978-86-331-3344-5', 1),
('Pop Cira i pop Spira',   'Stevan Sremac',      1894, '978-86-331-4455-6', 1),
('Orlovi rano lete',       'Branko Copic',       1957, '978-86-331-5566-7', 1),
('Magarece godine',        'Branko Copic',       1960, '978-86-331-6677-8', 0),
('Zlocin i kazna',         'Fjodor Dostojevski', 1866, '978-86-331-7788-9', 1),
('Idioti',                 'Fjodor Dostojevski', 1869, '978-86-331-8899-0', 1),
('Ana Karenjina',          'Lav Tolstoj',        1877, '978-86-331-9900-1', 1),
('Rat i mir',              'Lav Tolstoj',        1869, '978-86-331-1010-2', 0),
('1984',                   'George Orwell',      1949, '978-86-331-2020-3', 1),
('Zivotinjska farma',      'George Orwell',      1945, '978-86-331-3030-4', 1),
('Lovac u zitu',           'J.D. Salinger',      1951, '978-86-331-4040-5', 1),
('Veliki Getsbi',          'F. Scott Fitzgerald',1925, '978-86-331-5050-6', 0),
('Mali princ',             'Antoan de Sent Egziperi', 1943, '978-86-331-6060-7', 1),
('Alhemicar',              'Paulo Koeljo',       1988, '978-86-331-7070-8', 1);
GO