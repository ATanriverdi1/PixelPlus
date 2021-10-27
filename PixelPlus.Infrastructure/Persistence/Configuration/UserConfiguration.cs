using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelPlus.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlus.Infrastructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserAggregate>
    {
        public void Configure(EntityTypeBuilder<UserAggregate> builder)
        {
            builder.Property(p => p.UserName).HasMaxLength(10).IsRequired();
            builder.Property(p => p.Password).IsRequired();
            builder.HasIndex(p => p.UserName).IsUnique();
            builder.HasKey(p => p.Id);
        }
    }
}
