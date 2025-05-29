using Core.Entities.SocialFeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.SocialFeed
{
    public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            builder.HasIndex(r => new { r.UserId, r.PostId })
                .IsUnique();

            // Enum to int conversion for ReactionType
            builder.Property(r => r.ReactionType)
                .HasConversion<int>();
        }
    }
}
