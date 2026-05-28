using CarRentalApp.Data;
using CarRentalApp.Models;
using CarRentalApp.Models.ViewModels;
using CarRentalApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Services
{
    public class RentalService : IRentalService
    {
        private readonly ApplicationDbContext _ctx;
        public RentalService(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Rental>> GetAllRentals()
            => await _ctx.Rentals
                .Include(r=>r.Car).ThenInclude(c=>c!.CarCategory)
                .Include(r=>r.User).Include(r=>r.PickupLocation).Include(r=>r.ReturnLocation).Include(r=>r.Payment)
                .OrderByDescending(r=>r.StartDate).ToListAsync();

        public async Task<IEnumerable<Rental>> GetRentalsByUser(string userId)
            => await _ctx.Rentals.Where(r=>r.UserId==userId)
                .Include(r=>r.Car).ThenInclude(c=>c!.CarCategory)
                .Include(r=>r.PickupLocation).Include(r=>r.ReturnLocation).Include(r=>r.Payment)
                .OrderByDescending(r=>r.StartDate).ToListAsync();

        public async Task<Rental?> GetRentalById(int id)
            => await _ctx.Rentals.Include(r=>r.Car).Include(r=>r.User)
                .Include(r=>r.PickupLocation).Include(r=>r.ReturnLocation).Include(r=>r.Payment)
                .FirstOrDefaultAsync(r=>r.Id==id);

        public async Task RentCar(RentViewModel model, string userId)
        {
            var car = await _ctx.Cars.FindAsync(model.CarId) ?? throw new Exception("Masina nu a fost gasita.");
            if (!car.IsAvailable) throw new Exception("Masina nu este disponibila.");
            if (model.EndDate <= model.StartDate) throw new Exception("Data returnarii trebuie sa fie dupa data ridicarii.");

            int days = Math.Max(1, (model.EndDate - model.StartDate).Days);
            decimal total = days * car.PricePerDay;

            var rental = new Rental {
                CarId=model.CarId, UserId=userId,
                StartDate=model.StartDate, EndDate=model.EndDate,
                TotalPrice=total, Status=RentalStatus.Active,
                PickupLocationId=model.PickupLocationId, ReturnLocationId=model.ReturnLocationId,
                Payment = new Payment { Amount=total, PaymentDate=DateTime.Now, PaymentMethod=model.PaymentMethod??"Card", IsPaid=true }
            };
            car.IsAvailable = false;
            _ctx.Rentals.Add(rental);
            await _ctx.SaveChangesAsync();
        }

        public async Task EndRental(int id)
        {
            var r = await _ctx.Rentals.Include(r=>r.Car).FirstOrDefaultAsync(r=>r.Id==id)
                ?? throw new Exception("Inchirierea nu a fost gasita.");
            r.Status = RentalStatus.Completed;
            if (r.Car != null) r.Car.IsAvailable = true;
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteRental(int id)
        {
            var r = await _ctx.Rentals.Include(r=>r.Car).FirstOrDefaultAsync(r=>r.Id==id)
                ?? throw new Exception("Inchirierea nu a fost gasita.");
            if (r.Car != null && r.Status == RentalStatus.Active) r.Car.IsAvailable = true;
            _ctx.Rentals.Remove(r);
            await _ctx.SaveChangesAsync();
        }
    }
}
