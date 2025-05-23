using Entities.Models;
using Entities.Models.Authorization;
using Entities.Models.Feeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<User, Role, string>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Reaction>()
                   .HasIndex(l => new { l.UserId, l.PostId })
                   .IsUnique();

            // User -> Posts (Cascade delete posts when user is deleted)
            builder.Entity<User>()
                   .HasMany(u => u.Posts)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Post -> Likes (Cascade delete likes when post is deleted)
            builder.Entity<Post>()
                   .HasMany(p => p.Reactions)
                   .WithOne(l => l.Post)
                   .HasForeignKey(l => l.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            // User -> Likes (Cascade delete likes when user is deleted)
            builder.Entity<User>()
                   .HasMany(u => u.Reactions)
                   .WithOne(l => l.User)
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Post -> Attachments (Cascade delete attachments when post is deleted)
            builder.Entity<Post>()
                   .HasMany(p => p.Attachments)
                   .WithOne(a => a.Post)
                   .HasForeignKey(a => a.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

        // DbSets for other entities
        // public DbSet<SomeEntity> SomeEntities { get; set; }
    }
}
