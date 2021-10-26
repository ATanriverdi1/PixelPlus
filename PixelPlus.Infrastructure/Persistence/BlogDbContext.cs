using Microsoft.EntityFrameworkCore;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain.Blog;
using PixelPlus.Domain.Category;
using System;

namespace PixelPlus.Infrastructure.Persistence
{
    public class BlogDbContext : DbContext, IBlogDbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BlogAggregate> Blogs { get; set; }
        public DbSet<BlogCategoryRecord> BlogCategories { get; set; }
        public DbSet<CategoryAggregate> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
