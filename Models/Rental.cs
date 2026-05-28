using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Models
{
    public enum RentalStatus { Active, Completed, Cancelled }

    public class Rental
    {
        public int Id { get; set; }
        [Required] public int CarId { get; set; }
        public Car? Car { get; set; }
        [Required] public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
        [Required, Display(Name="Data start")] public DateTime StartDate { get; set; }
        [Required, Display(Name="Data retur")]  public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public RentalStatus Status { get; set; } = RentalStatus.Active;
        [Required] public int PickupLocationId { get; set; }
        public Location? PickupLocation { get; set; }
        [Required] public int ReturnLocationId { get; set; }
        public Location? ReturnLocation { get; set; }
        public Payment? Payment { get; set; }
        public int DaysCount => Math.Max(1, (EndDate - StartDate).Days);
    }
}
