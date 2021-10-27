using Microsoft.EntityFrameworkCore;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain;
using PixelPlus.Domain.Blog;
using PixelPlus.Domain.Category;
using PixelPlus.Domain.User;

namespace PixelPlus.Infrastructure.Persistence
{
    public class BlogDbContext : DbContext, IBlogDbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BlogAggregate> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<CategoryAggregate> Categories { get; set; }
        public DbSet<UserAggregate> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
