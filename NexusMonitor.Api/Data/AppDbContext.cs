using Microsoft.EntityFrameworkCore;

namespace NexusMonitor.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<Measurement> Measurements { get; set; }
    }
}