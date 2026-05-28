using CarRentalApp.Models;
using CarRentalApp.Models.ViewModels;
using CarRentalApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CarRentalApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm, RoleManager<IdentityRole> rm)
        { _userManager = um; _signInManager = sm; _roleManager = rm; }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email, Email = model.Email,
                FirstName = model.FirstName, LastName = model.LastName,
                CreatedAt = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("User"))
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result;
        }

        public async Task<SignInResult> Login(LoginViewModel model)
            => await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

        public async Task Logout() => await _signInManager.SignOutAsync();
    }
}
