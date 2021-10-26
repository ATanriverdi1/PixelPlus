using MediatR;
using Microsoft.EntityFrameworkCore;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.Category.Queries
{
    public class CategoryQuery : IRequest<List<CategoryAggregate>>
    {
        public class Handler : IRequestHandler<CategoryQuery, List<CategoryAggregate>>
        {
            private readonly IBlogDbContext _context;

            public Handler(IBlogDbContext context)
            {
                _context = context;
            }

            public async Task<List<CategoryAggregate>> Handle(CategoryQuery request, CancellationToken cancellationToken)
            {
                return await _context.Categories.ToListAsync(cancellationToken);
            }
        }
    }
}
