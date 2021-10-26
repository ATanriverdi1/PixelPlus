using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelPlus.Api.Configuration;
using PixelPlus.Application.Interfaces;
using PixelPlus.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelPlus.Api.Extensions
{
    public static class ServiceCollectionDatabaseExtensions
    {
        public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IBlogDbContext, BlogDbContext>(p =>
                p.UseSqlServer(
                    configuration.GetValue<string>(ConfigKeys.DatabaseConnection),
                    options => { options.MigrationsAssembly(typeof(BlogDbContext).Assembly.FullName); }));

            return services;
        }
    }
}
