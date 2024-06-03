using CommonService.Domain.Models;

namespace CommonService.Infrastructure.Context
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
            // modelBuilder.HasCollation("my_collation", locale: "en-u-ks-primary", provider: "icu", deterministic: false);
            // modelBuilder.HasPostgresExtension("citext");

            DataSeed.seed(modelBuilder);
        }

        
        public DbSet<Telebirr> Telebirr { get; set; }
        public DbSet<BaseObject> BaseObject { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<ExchangeRate> ExchangeRate { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<HolidayDefinition> HolidayDefinition { get; set; }
        public DbSet<Languge> Language { get; set; }
        public DbSet<Machine> Machine { get; set; }
        public DbSet<ObjectType> ObjectType { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<OrganizationAttachment> OrganizationAttachment { get; set; }
        public DbSet<OrganizationBankAccount> OrganizationBankAccount { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnit { get; set; }    
        public DbSet<Period> Period { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<SystemConfiguration> SystemConfiguration { get; set; }
        public DbSet<SystemLookup> SystemLookup { get; set; }
        public DbSet<TagDefinition> TagDefinition { get; set; }
        public DbSet<Tax> Tax { get; set; }
    }
}
