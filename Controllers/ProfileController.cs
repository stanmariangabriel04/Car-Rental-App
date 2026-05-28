using CarRentalApp.Models;
using CarRentalApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _um;
        public ProfileController(UserManager<ApplicationUser> um) => _um=um;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var u = await _um.GetUserAsync(User);
            if (u==null) return NotFound();
            return View(new ProfileViewModel { FirstName=u.FirstName, LastName=u.LastName, Email=u.Email, ProfileImagePath=u.ProfileImagePath });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            var u = await _um.GetUserAsync(User);
            if (u==null) return NotFound();
            if (model.ProfileImage != null && model.ProfileImage.Length > 0)
            {
                var dir = Path.Combine("wwwroot","images","profiles");
                Directory.CreateDirectory(dir);
                var name = Guid.NewGuid() + Path.GetExtension(model.ProfileImage.FileName);
                await using var s = System.IO.File.Create(Path.Combine(dir,name));
                await model.ProfileImage.CopyToAsync(s);
                u.ProfileImagePath = "/images/profiles/"+name;
            }
            u.FirstName = model.FirstName;
            u.LastName  = model.LastName;
            await _um.UpdateAsync(u);
            ViewBag.Success = "Profilul a fost actualizat!";
            model.ProfileImagePath = u.ProfileImagePath;
            return View(model);
        }
    }
}
