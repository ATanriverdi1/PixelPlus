using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelPlus.Domain.Blog;

namespace PixelPlus.Infrastructure.Persistence.Configuration
{
    public class BlogConfiguration : IEntityTypeConfiguration<BlogAggregate>
    {
        public void Configure(EntityTypeBuilder<BlogAggregate> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Content).HasMaxLength(20).IsRequired();
            builder.HasKey("Id");
        }
    }
}
