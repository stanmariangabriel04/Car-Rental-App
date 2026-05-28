using CarRentalApp.Data;
using CarRentalApp.Models;
using CarRentalApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _ctx;
        public CarService(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Car>> GetAllCars()
            => await _ctx.Cars.Include(c=>c.CarCategory).Include(c=>c.Location).Include(c=>c.Reviews).OrderBy(c=>c.Brand).ToListAsync();

        public async Task<IEnumerable<Car>> GetAvailableCars()
            => await _ctx.Cars.Where(c=>c.IsAvailable).Include(c=>c.CarCategory).Include(c=>c.Location).Include(c=>c.Reviews).OrderBy(c=>c.PricePerDay).ToListAsync();

        public async Task<Car?> GetCarById(int id)
            => await _ctx.Cars.Include(c=>c.CarCategory).Include(c=>c.Location).Include(c=>c.Reviews).ThenInclude(r=>r.User).FirstOrDefaultAsync(c=>c.Id==id);

        public async Task CreateCar(Car car) { _ctx.Cars.Add(car); await _ctx.SaveChangesAsync(); }
        public async Task UpdateCar(Car car) { _ctx.Cars.Update(car); await _ctx.SaveChangesAsync(); }

        public async Task DeleteCar(int id)
        {
            var car = await _ctx.Cars.FindAsync(id);
            if (car != null) { _ctx.Cars.Remove(car); await _ctx.SaveChangesAsync(); }
        }
    }
}
