using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required] public string Brand { get; set; } = string.Empty;
        [Required] public string Model { get; set; } = string.Empty;
        [Required, Range(1900,2030)] public int Year { get; set; }
        [Required, Range(1,10000), Display(Name="Pret/zi (RON)")] public decimal PricePerDay { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; } = true;
        [Required, Display(Name="Categorie")] public int CarCategoryId { get; set; }
        public CarCategory? CarCategory { get; set; }
        [Required, Display(Name="Locatie")] public int LocationId { get; set; }
        public Location? Location { get; set; }
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public double AverageRating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
    }
}
