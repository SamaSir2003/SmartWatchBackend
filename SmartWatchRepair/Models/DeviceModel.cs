namespace SmartWatchRepair.Models
{
    public class DeviceModel
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string ModelName { get; set; }

        // Navigation Property
        public virtual Brand Brand { get; set; }    
    }
}
