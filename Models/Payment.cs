namespace CarRentalApp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public Rental? Rental { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string PaymentMethod { get; set; } = "Card";
        public bool IsPaid { get; set; } = true;
    }
}
