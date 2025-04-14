namespace SmartWatchRepair.Models
{
    public class RepairIssue
    {
        public int Id { get; set; }
        public int DeviceModelId { get; set; }
        public string IssueName { get; set; }
        public decimal BasePrice { get; set; }

        // Navigation Property
        public virtual DeviceModel DeviceModel { get; set; }
    }
}
