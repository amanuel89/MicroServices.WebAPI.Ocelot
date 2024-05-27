
using ConsigneeService.Domain.Models;

namespace ConsigneeService.Infrastructure.Context
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
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Telebirr> Telebirr { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }
}
