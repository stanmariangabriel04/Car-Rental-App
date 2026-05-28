using CarRentalApp.Data;
using CarRentalApp.Models.ViewModels;
using CarRentalApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarRentalApp.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        private readonly IRentalService _rentals;
        private readonly ICarService _cars;
        private readonly ApplicationDbContext _ctx;

        public RentalsController(IRentalService rentals, ICarService cars, ApplicationDbContext ctx)
        { _rentals=rentals; _cars=cars; _ctx=ctx; }

        public async Task<IActionResult> MyRentals()
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return View(await _rentals.GetRentalsByUser(uid));
        }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Index()
            => View(await _rentals.GetAllRentals());

        public async Task<IActionResult> Rent(int carId)
        {
            var car = await _cars.GetCarById(carId);
            if (car == null || !car.IsAvailable) return NotFound();
            return View(new RentViewModel
            {
                CarId=carId, Car=car,
                StartDate=DateTime.Today, EndDate=DateTime.Today.AddDays(1),
                Locations=await _ctx.Locations.ToListAsync()
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Rent(RentViewModel model)
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (ModelState.IsValid)
            {
                try { await _rentals.RentCar(model,uid); TempData["Success"]="Masina a fost inchiriata cu succes!"; return RedirectToAction(nameof(MyRentals)); }
                catch (Exception ex) { ModelState.AddModelError("",ex.Message); }
            }
            model.Car = await _cars.GetCarById(model.CarId);
            model.Locations = await _ctx.Locations.ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> EndRental(int id)
        { await _rentals.EndRental(id); TempData["Success"]="Masina a fost returnata cu succes!"; return RedirectToAction(nameof(MyRentals)); }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(int id)
        { var r=await _rentals.GetRentalById(id); return r==null ? NotFound() : View(r); }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { await _rentals.DeleteRental(id); return RedirectToAction(nameof(Index)); }
    }
}
