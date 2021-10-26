using MediatR;
using PixelPlus.Application.Blog.Enums;
using PixelPlus.Application.Blog.Queries;
using PixelPlus.Application.Category.Queries;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain;
using PixelPlus.Domain.Exceptions;
using System;
using System.Collections.Generic;
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
                var blogCategoryGet = await _mediator.Send(new BlogByIdAndCategoryIdQuery(request.Id, request.CategoryId), cancellationToken);

                if(blogCategoryGet != null)
                    throw new BusinessException(BlogApplicationException.ThereIsAlreadyACategoryExists,
                        new KeyValuePair<string, string>("categoryId", blogCategoryGet.CategoryAggregateId.ToString()));

                var blogCategory = new BlogCategory(request.Id, request.CategoryId);
                await _context.BlogCategories.AddAsync(blogCategory, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
