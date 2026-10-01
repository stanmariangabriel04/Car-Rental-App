# CarRental - Web Application Design Project

## Steps to run

### 1. Open the solution

Open `CarRentalApp.sln` in Visual Studio.

### 2. Check the connection string

In CarRentalApp/appsettings.json, the default connection string is:

Server=(localdb)\mssqllocaldb;Database=CarRentalDb;Trusted_Connection=True

### 3. Apply migrations (Package Manager Console)
Add-Migration InitialCreate

Update-Database


### 4. Run the application

Press F5 or Ctrl+F5.

## Admin account (created automatically on first run)

- **Email:** admin@carrental.ro
- **Password:** Admin123!

## Database structure (6 tables + Identity)

 Table          Description

* Cars: Available cars
* CarCategories: Sedan, SUV, Sport, Electric, Minivan
* Locations: Pickup/return locations
* Rentals: Rentals made by users
* Payments: Payments associated with rentals
* Reviews: User reviews

## Roles

- **Admin** - full access (CRUD on cars, all rentals)
- **User** - rents cars, leaves reviews, manages profile

## Implemented pages

1. Home/Index - Landing page
2. Cars/Index - Car list with filtering
3. Cars/Details - Car details + reviews
4. Account/Login - Styled login form
5. Account/Register - Styled registration form
6. Rentals/MyRentals - User's rentals
7. Profile/Index - Profile + photo upload

**Admin extra:**

- Cars/Create, Cars/Edit, Cars/Delete
- Rentals/Index (all rentals)
