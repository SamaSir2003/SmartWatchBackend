namespace SmartWatchRepair.Models
{
    public class User
    {
        public string Id { get; set; }  // Primary Key
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }  // Admin, Customer, Technician
        public string Password { get; set; }  // Store hash of the password
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
