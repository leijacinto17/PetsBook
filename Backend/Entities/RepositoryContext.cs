using System.Security.Cryptography;
using Entities.Models;
using Entities.Models.Authorization;
using Entities.Models.Feeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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
             .HasIndex(r => new { r.UserId, r.PostId })
             .IsUnique();

            builder.Entity<Reaction>()
                   .Property(r => r.ReactionType)
                   .HasConversion<int>();

            // User -> Posts (Cascade delete posts when user is deleted)
            builder.Entity<User>()
                   .HasMany(u => u.Posts)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Post -> Reactions (Restrict to avoid multiple cascade paths)
            builder.Entity<Post>()
                   .HasMany(p => p.Reactions)
                   .WithOne(r => r.Post)
                   .HasForeignKey(r => r.PostId)
                   .OnDelete(DeleteBehavior.Restrict); 

            // User -> Reactions (Set UserId to null when user is deleted)
            builder.Entity<User>()
                   .HasMany(u => u.Reactions)
                   .WithOne(r => r.User)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Post -> Attachments (Cascade delete attachments when post is deleted)
            builder.Entity<Post>()
                   .HasMany(p => p.Attachments)
                   .WithOne(a => a.Post)
                   .HasForeignKey(a => a.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

        // DbSets for other entities
        public DbSet<Post> Posts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }

    }
}
