

namespace SmartWatchRepair.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public string TransactionId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }  // COD, Online
        public string Status { get; set; }  // Success, Failed, Cancelled

        // Navigation Property
        public virtual Booking Booking { get; set; }
    }
}