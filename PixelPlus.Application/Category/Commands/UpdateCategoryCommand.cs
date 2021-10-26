using MediatR;
using PixelPlus.Application.Category.Queries;
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
    public class UpdateCategoryCommand : IRequest
    {
        public UpdateCategoryCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public class Handler : IRequestHandler<UpdateCategoryCommand>
        {
            private readonly IBlogDbContext _context;
            private readonly IMediator _mediator;
            public Handler(IBlogDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _mediator.Send(new CategoryByIdQuery(request.Id), cancellationToken);
                category.Update(request.Name);
                _context.Categories.Update(category);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
