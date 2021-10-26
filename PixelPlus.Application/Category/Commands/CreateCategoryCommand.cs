using MediatR;
using PixelPlus.Application.Interfaces;
using PixelPlus.Domain.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.Category.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryAggregate>
    {
        public CreateCategoryCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public class Handler : IRequestHandler<CreateCategoryCommand, CategoryAggregate>
        {
            private readonly IBlogDbContext _context;

            public Handler(IBlogDbContext context)
            {
                _context = context;
            }

            public async Task<CategoryAggregate> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = new CategoryAggregate(request.Name);
                await _context.Categories.AddAsync(category, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return category;
            }
        }
    }
}
