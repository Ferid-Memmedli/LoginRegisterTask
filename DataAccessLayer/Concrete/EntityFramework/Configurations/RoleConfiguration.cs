using EntitiesLayer.Concrete.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLayer.Constants;

namespace DataAccessLayer.Concrete.EntityFramework.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasIndex(r => r.NormalizedName).IsUnique();
            builder.Property(u => u.NormalizedName).IsRequired();

            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            builder.HasIndex(u => u.Name).IsUnique();
            builder.Property(u => u.Name).IsRequired();

            // Seed Data
            const string guid = "e654aabb-22f6-44c2-ac8b-189b01d33ebc";
            List<Role> roles = new()
            {
                new Role
                {
                    Name = RoleConstant.Admin,
                    NormalizedName = RoleConstant.Admin.ToUpper(),
                    ConcurrencyStamp = guid
                },// 1             
                 new Role
                {
                    Name = RoleConstant.Guest,
                    NormalizedName = RoleConstant.Guest.ToUpper(),
                    ConcurrencyStamp = guid,
                },// 2
            };

            int counter = 1;
            roles.ForEach(role =>
            {
                role.Id = counter;
                counter++;
            });
            builder.HasData(roles);
        }
    }
}
