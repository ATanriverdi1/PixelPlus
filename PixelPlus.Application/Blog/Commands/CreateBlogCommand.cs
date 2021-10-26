using MediatR;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.Blog.Commands
{
    public class CreateBlogCommand : IRequest<BlogAggregate>
    {
        public CreateBlogCommand(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public string Title { get; set; }
        public string Content { get; set; }

        public class Handler : IRequestHandler<CreateBlogCommand, BlogAggregate>
        {
            private readonly IBlogDbContext _context;

            public Handler(IBlogDbContext context)
            {
                _context = context;
            }

            public async Task<BlogAggregate> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
            {
                var blog = new BlogAggregate(request.Title, request.Content);
                await _context.Blogs.AddAsync(blog, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return blog;
            }
        }
    }
}
