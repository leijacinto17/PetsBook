using System.Reflection.Emit;
using System.Reflection;
using Core.Entities.Authorization;
using Core.Entities.Identity;
using Core.Entities.SocialFeed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User, Role, string>(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        // DbSets for other entities
        public DbSet<Post> Posts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }

    }
}