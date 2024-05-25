
using RideBackend.Domain.Models;

namespace RideBackend.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasCollation("my_collation", locale: "en-u-ks-primary", provider: "icu", deterministic: false);
            //modelBuilder.HasPostgresExtension("citext");

            DataSeed.seed(modelBuilder);
        }
        public DbSet<Address> Address { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Passenger> Passenger { get; set; }      
        public DbSet<VerificationCodes> VerificationCodes { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Telebirr> Telebirr { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<VehicleTypes> VehicleTypes { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<RideSettings> RideSettings { get; set; }
    }
}
