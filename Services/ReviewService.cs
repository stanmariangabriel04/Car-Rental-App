using CarRentalApp.Data;
using CarRentalApp.Models;
using CarRentalApp.Services.Interfaces;

namespace CarRentalApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _ctx;
        public ReviewService(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task AddReview(Review review)
        { review.CreatedAt = DateTime.Now; _ctx.Reviews.Add(review); await _ctx.SaveChangesAsync(); }

        public async Task DeleteReview(int id)
        { var r = await _ctx.Reviews.FindAsync(id); if (r!=null) { _ctx.Reviews.Remove(r); await _ctx.SaveChangesAsync(); } }
    }
}
