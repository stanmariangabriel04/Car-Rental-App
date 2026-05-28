using CarRentalApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _cars;
        public HomeController(ICarService cars) => _cars = cars;

        public async Task<IActionResult> Index()
            => View((await _cars.GetAvailableCars()).Take(6));

        public IActionResult AccessDenied() => View();
    }
}
