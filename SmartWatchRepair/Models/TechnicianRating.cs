namespace SmartWatchRepair.Models
{
    public class TechnicianRating
    {
        public int Id { get; set; }
        public string TechnicianId { get; set; }  // Foreign Key to User
        public int BookingId { get; set; }  // Foreign Key to Booking
        public int Rating { get; set; }  // Rating between 1 and 5
        public string Feedback { get; set; }
        public DateTime RatedOn { get; set; }

        // Navigation Properties
        public virtual User Technician { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
