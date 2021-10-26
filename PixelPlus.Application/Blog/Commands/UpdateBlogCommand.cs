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
    public class UpdateBlogCommand : IRequest
    {
        public UpdateBlogCommand(Guid id, string title, string content)
        {
            Id = id;
            Title = title;
            Content = content;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public class Handler : IRequestHandler<UpdateBlogCommand>
        {
            private readonly IBlogDbContext _context;
            private readonly IMediator _mediator;
            public Handler(IBlogDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
            {
                var blog = await _mediator.Send(new BlogByIdQuery(request.Id));
                blog.Update(request.Title, request.Content);
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
