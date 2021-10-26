using MediatR;
using Microsoft.EntityFrameworkCore;
using PixelPlus.Application.Blog.Enums;
using PixelPlus.Application.Category.Enums;
using PixelPlus.Application.Exceptions;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain;
using PixelPlus.Domain.Blog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.Blog.Queries
{
    public class BlogByIdAndCategoryIdQuery : IRequest<BlogCategory>
    {
        public BlogByIdAndCategoryIdQuery(Guid blogId, Guid categoryId)
        {
            BlogId = blogId;
            CategoryId = categoryId;
        }

        public Guid BlogId { get; set; }
        public Guid CategoryId { get; set; }

        public class Handler : IRequestHandler<BlogByIdAndCategoryIdQuery, BlogCategory>
        {
            private readonly IBlogDbContext _context;

            public Handler(IBlogDbContext context)
            {
                _context = context;
            }

            public async Task<BlogCategory> Handle(BlogByIdAndCategoryIdQuery request, CancellationToken cancellationToken)
            {
                var blog = await _context.Blogs.FirstOrDefaultAsync(p => p.Id == request.BlogId, cancellationToken);
                var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == request.CategoryId, cancellationToken);

                if (blog == null)
                    throw new NotFoundException(BlogApplicationException.BlogNotFound,
                        new KeyValuePair<string, string>("blogId", request.BlogId.ToString()));

                if (category == null)
                    throw new NotFoundException(CategoryApplicationException.CategoryNotFound,
                        new KeyValuePair<string, string>("categoryId", request.CategoryId.ToString()));

                var blogCategory = await _context.BlogCategories.FirstOrDefaultAsync(p => p.CategoryAggregateId == category.Id && p.BlogAggregateId == blog.Id, cancellationToken);

                if (blogCategory == null) return null;

                return blogCategory;
            }
        }
    }
}
