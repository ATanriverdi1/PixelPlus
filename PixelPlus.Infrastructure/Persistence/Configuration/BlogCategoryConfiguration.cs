using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelPlus.Domain.Blog;

namespace PixelPlus.Infrastructure.Persistence.Configuration
{
    public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategoryRecord>
    {
        public void Configure(EntityTypeBuilder<BlogCategoryRecord> builder)
        {
            builder.HasOne(p => p.Blog).WithMany(p => p.Categories).HasForeignKey(p => p.BlogId);
            builder.HasOne(p => p.Category).WithMany(p => p.Blogs).HasForeignKey(p => p.CategoryId);
            builder.HasKey(p => new { p.BlogId, p.CategoryId });
        }
    }
}
