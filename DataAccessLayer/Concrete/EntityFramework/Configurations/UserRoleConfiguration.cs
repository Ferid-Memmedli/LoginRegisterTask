using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Concrete.EntityFramework.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.HasKey(r => new { r.UserId, r.RoleId });

            IdentityUserRole<int> userROle = new()
            {
                UserId = 1,
                RoleId = 1
            };

            builder.HasData(userROle);

        }
    }
}
