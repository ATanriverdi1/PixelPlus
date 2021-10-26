using MediatR;
using PixelPlus.Application.Blog.Queries;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain.Blog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.Blog.Commands
{
    public class RemoveCategoryFromBlogCommand : IRequest
    {
        public RemoveCategoryFromBlogCommand(Guid blogId, Guid categoryId)
        {
            CategoryId = categoryId;
            BlogId = blogId;
        }

        public Guid CategoryId { get; set; }
        public Guid BlogId { get; set; }

        public class Handler : IRequestHandler<RemoveCategoryFromBlogCommand>
        {
            private readonly IBlogDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IBlogDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(RemoveCategoryFromBlogCommand request, CancellationToken cancellationToken)
            {
                var blogCategory = await _mediator.Send(new BlogByIdAndCategoryIdQuery(request.BlogId, request.CategoryId), cancellationToken);

                if (blogCategory == null) return Unit.Value;

                _context.BlogCategories.Remove(blogCategory);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
