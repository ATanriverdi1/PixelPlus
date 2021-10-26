using MediatR;
using Microsoft.EntityFrameworkCore;
using PixelPlus.Application.Blog.Queries;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain.Blog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.Blog.Commands
{
    public class RemoveBlogCommand : IRequest
    {
        public RemoveBlogCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public class Handler : IRequestHandler<RemoveBlogCommand>
        {
            private readonly IBlogDbContext _context;
            private readonly IMediator _mediator;
            public Handler(IBlogDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(RemoveBlogCommand request, CancellationToken cancellationToken)
            {
                var blog = await _context.Blogs.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                if (blog == null) return Unit.Value;

                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
