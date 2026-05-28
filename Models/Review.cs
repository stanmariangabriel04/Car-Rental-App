using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required] public int CarId { get; set; }
        public Car? Car { get; set; }
        [Required] public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
        [Required, Range(1,5)] public int Rating { get; set; }
        [Required, MaxLength(500)] public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
