using CarRentalApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarRentalApp.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<SignInResult> Login(LoginViewModel model);
        Task Logout();
    }
}
