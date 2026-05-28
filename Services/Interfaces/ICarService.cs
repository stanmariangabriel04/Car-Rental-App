using CarRentalApp.Models;

namespace CarRentalApp.Services.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCars();
        Task<IEnumerable<Car>> GetAvailableCars();
        Task<Car?> GetCarById(int id);
        Task CreateCar(Car car);
        Task UpdateCar(Car car);
        Task DeleteCar(int id);
    }
}
