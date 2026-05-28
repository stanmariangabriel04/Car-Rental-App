using CarRentalApp.Models;
using CarRentalApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRentalApp.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviews;
        public ReviewsController(IReviewService reviews) => _reviews=reviews;

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            review.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _reviews.AddReview(review);
            return RedirectToAction("Details","Cars",new{id=review.CarId});
        }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(int id, int carId)
        { await _reviews.DeleteReview(id); return RedirectToAction("Details","Cars",new{id=carId}); }
    }
}
