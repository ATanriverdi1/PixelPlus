using MediatR;
using Microsoft.EntityFrameworkCore;
using PixelPlus.Application.Blog.Queries;
using PixelPlus.Application.Category.Queries;
using PixelPlus.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.Blog.Commands
{
    public class AddCategoryToBlogCommand : IRequest
    {
        public AddCategoryToBlogCommand(Guid id, Guid categoryId)
        {
            Id = id;
            CategoryId = categoryId;
        }

        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }

        public class Handler : IRequestHandler<AddCategoryToBlogCommand>
        {
            private readonly IMediator _mediator;
            private readonly IBlogDbContext _context;

            public Handler(IBlogDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(AddCategoryToBlogCommand request, CancellationToken cancellationToken)
            {
                var category = await _mediator.Send(new CategoryByIdQuery(request.CategoryId), cancellationToken);
                var blog = await _mediator.Send(new BlogByIdQuery(request.Id), cancellationToken);
                var blogCategory = blog.AddCategory(category.Id);
                _context.BlogCategories.Update(blogCategory);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
