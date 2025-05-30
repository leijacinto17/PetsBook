using Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Role
{
    public class RoleConfiguration : IEntityTypeConfiguration<Core.Entities.Authorization.Role>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Authorization.Role> builder)
        {
        }
    }
}
