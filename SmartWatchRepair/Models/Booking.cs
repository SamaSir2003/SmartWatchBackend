namespace SmartWatchRepair.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // Foreign Key to User
        public int DeviceModelId { get; set; }
        public int RepairIssueId { get; set; }
        public string Status { get; set; }  // Pending, Confirmed, In Progress, Completed
        public DateTime BookingDate { get; set; }
        public DateTime EstimatedDelivery { get; set; }
        public decimal TotalPrice { get; set; }
        public string TechnicianId { get; set; }  // Optional, can be assigned later

        //Navigation Properties
        //public virtual User User { get; set; }
        //public virtual DeviceModel DeviceModel { get; set; }
        //public virtual RepairIssue RepairIssue { get; set; }
    }
}
