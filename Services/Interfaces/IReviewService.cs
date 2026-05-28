using CarRentalApp.Models;

namespace CarRentalApp.Services.Interfaces
{
    public interface IReviewService
    {
        Task AddReview(Review review);
        Task DeleteReview(int id);
    }
}
