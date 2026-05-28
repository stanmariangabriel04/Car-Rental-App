namespace CarRentalApp.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ProfileImagePath { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
