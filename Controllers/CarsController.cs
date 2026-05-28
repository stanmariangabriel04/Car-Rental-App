using CarRentalApp.Data;
using CarRentalApp.Models;
using CarRentalApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _cars;
        private readonly ApplicationDbContext _ctx;

        public CarsController(ICarService cars, ApplicationDbContext ctx)
        { _cars = cars; _ctx = ctx; }

        public async Task<IActionResult> Index() => View(await _cars.GetAllCars());

        public async Task<IActionResult> Details(int id)
        {
            var car = await _cars.GetCarById(id);
            return car == null ? NotFound() : View(car);
        }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Create()
        { await FillDropdowns(); return View(); }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles="Admin")]
        public async Task<IActionResult> Create(Car car, IFormFile? imageFile)
        {
            ModelState.Remove("CarCategory"); ModelState.Remove("Location"); ModelState.Remove("ImageUrl");
            if (ModelState.IsValid)
            {
                car.ImageUrl = await SaveImg(imageFile) ?? "/images/cars/default-car.png";
                await _cars.CreateCar(car);
                return RedirectToAction(nameof(Index));
            }
            await FillDropdowns(car.CarCategoryId, car.LocationId);
            return View(car);
        }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _cars.GetCarById(id);
            if (car == null) return NotFound();
            await FillDropdowns(car.CarCategoryId, car.LocationId);
            return View(car);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(int id, Car car, IFormFile? imageFile)
        {
            if (id != car.Id) return NotFound();
            ModelState.Remove("CarCategory"); ModelState.Remove("Location"); ModelState.Remove("ImageUrl");
            if (ModelState.IsValid)
            {
                var img = await SaveImg(imageFile);
                if (img != null) car.ImageUrl = img;
                await _cars.UpdateCar(car);
                return RedirectToAction(nameof(Index));
            }
            await FillDropdowns(car.CarCategoryId, car.LocationId);
            return View(car);
        }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _cars.GetCarById(id);
            return car == null ? NotFound() : View(car);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { await _cars.DeleteCar(id); return RedirectToAction(nameof(Index)); }

        private async Task FillDropdowns(int? catId=null, int? locId=null)
        {
            ViewBag.CarCategoryId = new SelectList(await _ctx.CarCategories.ToListAsync(),"Id","Name",catId);
            ViewBag.LocationId    = new SelectList(await _ctx.Locations.ToListAsync(),"Id","Name",locId);
        }

        private async Task<string?> SaveImg(IFormFile? f)
        {
            if (f == null || f.Length == 0) return null;
            var dir = Path.Combine("wwwroot","images","cars");
            Directory.CreateDirectory(dir);
            var name = Guid.NewGuid() + Path.GetExtension(f.FileName);
            await using var s = System.IO.File.Create(Path.Combine(dir,name));
            await f.CopyToAsync(s);
            return "/images/cars/" + name;
        }
    }
}
