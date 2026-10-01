# CarRental - Proiect PAW

## Pași pentru rulare

### 1. Deschide solutia
Deschide `CarRentalApp.sln` in Visual Studio 2022.

### 2. Verifica connection string
In `CarRentalApp/appsettings.json`, connection string-ul implicit este:
```
Server=(localdb)\mssqllocaldb;Database=CarRentalDb;Trusted_Connection=True
```

### 3. Aplica migratiile (Package Manager Console)
```
Add-Migration InitialCreate
Update-Database
```

### 4. Ruleaza aplicatia
Apasa F5 sau Ctrl+F5.

## Cont Admin (creat automat la prima rulare)
- **Email:** admin@carrental.ro
- **Parola:** Admin123!

## Structura BD (6 tabele + Identity)
| Tabel | Descriere |
|-------|-----------|
| Cars | Masinile disponibile |
| CarCategories | Sedan, SUV, Sport, Electric, Minivan |
| Locations | Locatii ridicare/returnare |
| Rentals | Inchirierile facute |
| Payments | Platile aferente |
| Reviews | Recenzii utilizatori |

## Roluri
- **Admin** - acces complet (CRUD masini, toate inchirierile)
- **User** - inchiriaza masini, lasa recenzii, gestioneaza profil

## Pagini implementate
1. Home/Index - Landing page
2. Cars/Index - Lista masini cu filtrare
3. Cars/Details - Detalii masina + recenzii
4. Account/Login - Formular login stilizat
5. Account/Register - Formular register stilizat
6. Rentals/MyRentals - Inchirierile utilizatorului
7. Profile/Index - Profil + upload poza

**Admin extra:**
- Cars/Create, Cars/Edit, Cars/Delete
- Rentals/Index (toate inchirierile)
