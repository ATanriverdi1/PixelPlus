using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelPlus.Domain;
using PixelPlus.Domain.Blog;

namespace PixelPlus.Infrastructure.Persistence.Configuration
{
    public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
    {
        public void Configure(EntityTypeBuilder<BlogCategory> builder)
        {
            builder.HasOne(p => p.Blog).WithMany(p => p.Categories).HasForeignKey(p => p.BlogAggregateId);
            builder.HasOne(p => p.Category).WithMany(p => p.Blogs).HasForeignKey(p => p.CategoryAggregateId);
            builder.HasKey(p => new { p.BlogAggregateId, p.CategoryAggregateId });
        }
    }
}
