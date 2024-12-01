namespace RentalDeliverer.src.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Deliverer> Deliverers { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalType> RentalTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DelivererConfiguration());
            modelBuilder.ApplyConfiguration(new MotorcycleConfiguration());
            modelBuilder.ApplyConfiguration(new RentalConfiguration());
            modelBuilder.ApplyConfiguration(new RentalTypeConfiguration());
        }
    }
}
