using EntitiesLayer.Concrete.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasIndex(u => u.NormalizedUserName).IsUnique();
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.NormalizedUserName).IsRequired();

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).IsUnique();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.NormalizedEmail).IsRequired();

            builder.HasIndex(u => u.PhoneNumber).IsUnique();
            builder.Property(u => u.PhoneNumber).IsRequired(false).HasColumnType("char(12)");

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);

            var passwordHasher = new PasswordHasher<User>();

            var admin = new User()
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                FirstName = "Flankes",
                LastName = "Flankesov",
                PhoneNumber = "994552393347",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = "3317689B-4358-4F80-874E-D8B6EAEF0AE4"
            };

            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123");

            builder.HasData(admin);
        }
    }
}
