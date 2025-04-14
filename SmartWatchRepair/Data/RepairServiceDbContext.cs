using Microsoft.EntityFrameworkCore;
using SmartWatchRepair.Models;

namespace SmartWatchRepair.Data
{
    public class RepairServiceDbContext : DbContext
    {
        public RepairServiceDbContext(DbContextOptions<RepairServiceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<DeviceModel> DeviceModels { get; set; }
        public DbSet<RepairIssue> RepairIssues { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TechnicianRating> TechnicianRatings { get; set; }

        // Optionally override OnModelCreating() for specific configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add further configurations if needed (e.g., relationships)
        }
    }
}