using Microsoft.EntityFrameworkCore;
using PixelPlus.Domain;
using PixelPlus.Domain.Blog;
using PixelPlus.Domain.Category;
using PixelPlus.Domain.User;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.Interfaces
{
    public interface IBlogDbContext
    {
        DbSet<BlogAggregate> Blogs { get; set; }
        DbSet<BlogCategory> BlogCategories { get; set; }
        DbSet<CategoryAggregate> Categories { get; set; }
        DbSet<UserAggregate> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
