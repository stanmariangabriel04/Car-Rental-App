using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Models.ViewModels
{
    public class RentViewModel
    {
        public int CarId { get; set; }
        public Car? Car { get; set; }

        [Required(ErrorMessage="Data de start este obligatorie")]
        [Display(Name="Data ridicare"), DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage="Data de retur este obligatorie")]
        [Display(Name="Data returnare"), DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

        [Required(ErrorMessage="Selectati locatia de ridicare")]
        [Display(Name="Locatie ridicare")]
        public int PickupLocationId { get; set; }

        [Required(ErrorMessage="Selectati locatia de returnare")]
        [Display(Name="Locatie returnare")]
        public int ReturnLocationId { get; set; }

        public string PaymentMethod { get; set; } = "Card";
        public List<Location>? Locations { get; set; }
    }
}
