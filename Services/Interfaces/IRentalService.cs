using CarRentalApp.Models;
using CarRentalApp.Models.ViewModels;

namespace CarRentalApp.Services.Interfaces
{
    public interface IRentalService
    {
        Task<IEnumerable<Rental>> GetAllRentals();
        Task<IEnumerable<Rental>> GetRentalsByUser(string userId);
        Task<Rental?> GetRentalById(int id);
        Task RentCar(RentViewModel model, string userId);
        Task EndRental(int rentalId);
        Task DeleteRental(int id);
    }
}
