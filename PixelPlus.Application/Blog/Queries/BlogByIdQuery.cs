using MediatR;
using Microsoft.EntityFrameworkCore;
using PixelPlus.Application.Blog.Enums;
using PixelPlus.Application.Exceptions;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain.Blog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.Blog.Queries
{
    public class BlogByIdQuery : IRequest<BlogAggregate>
    {
        public BlogByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public class Handler : IRequestHandler<BlogByIdQuery, BlogAggregate>
        {
            private readonly IBlogDbContext _context;

            public Handler(IBlogDbContext context)
            {
                _context = context;
            }

            public async Task<BlogAggregate> Handle(BlogByIdQuery request, CancellationToken cancellationToken)
            {
                var blog = await _context.Blogs.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                if (blog == null)
                    throw new NotFoundException(BlogApplicationException.BlogNotFound,
                        new KeyValuePair<string, string>("id", request.Id.ToString()));
                
                return blog;
            }
        }
    }
}
