using Core.Entities.SocialFeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.SocialFeed
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Post -> Reactions (Restrict to avoid multiple cascade paths)
            builder.HasMany(p => p.Reactions)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            // Post -> Attachments (Cascade delete)
            builder.HasMany(p => p.Attachments)
                .WithOne(a => a.Post)
                .HasForeignKey(a => a.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
