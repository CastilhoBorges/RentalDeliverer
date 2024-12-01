namespace RentalDeliverer.src.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Mail)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Mail)
                .IsUnique();
            
            builder.HasOne(u => u.Deliverer)
                .WithOne(d => d.User)
                .HasForeignKey<Deliverer>(d => d.UserId)
                .IsRequired(false);
        }
    }
}
