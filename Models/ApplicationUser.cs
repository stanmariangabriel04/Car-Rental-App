using Microsoft.AspNetCore.Identity;

namespace CarRentalApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImagePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
