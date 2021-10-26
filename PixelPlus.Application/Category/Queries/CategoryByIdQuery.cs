using MediatR;
using Microsoft.EntityFrameworkCore;
using PixelPlus.Application.Category.Enums;
using PixelPlus.Application.Exceptions;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain.Category;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.Category.Queries
{
    public class CategoryByIdQuery : IRequest<CategoryAggregate>
    {
        public CategoryByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public class Handler : IRequestHandler<CategoryByIdQuery, CategoryAggregate>
        {
            private readonly IBlogDbContext _context;

            public Handler(IBlogDbContext context)
            {
                _context = context;
            }

            public async Task<CategoryAggregate> Handle(CategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                if (category == null)
                    throw new NotFoundException(CategoryApplicationException.CategoryNotFound,
                        new KeyValuePair<string, string>("id", request.Id.ToString()));

                return category;
            }
        }
    }
}
