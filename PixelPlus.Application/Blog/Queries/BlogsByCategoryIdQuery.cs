using MediatR;
using Microsoft.EntityFrameworkCore;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.Blog.Queries
{
    public class BlogsByCategoryIdQuery : IRequest<List<BlogAggregate>>
    {
        public BlogsByCategoryIdQuery(Guid? categoryId)
        {
            CategoryId = categoryId;
        }

        public Guid? CategoryId { get; set; }

        public class Handler : IRequestHandler<BlogsByCategoryIdQuery, List<BlogAggregate>>
        {
            private readonly IBlogDbContext _context;

            public Handler(IBlogDbContext context)
            {
                _context = context;
            }

            public async Task<List<BlogAggregate>> Handle(BlogsByCategoryIdQuery request, CancellationToken cancellationToken)
            {
                if (!request.CategoryId.HasValue)
                    return await _context.Blogs.ToListAsync(cancellationToken);

                var ids = await _context.BlogCategories.Where(p => p.CategoryAggregateId == request.CategoryId).Select(p => p.BlogAggregateId).ToListAsync(cancellationToken);

                if (ids == null || ids.Count == 0) return null;

                return await _context.Blogs.Where(p => ids.Contains(p.Id)).ToListAsync(cancellationToken); 
            }
        }
    }
}
