using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class RemoveCategoryCommand : IRequest
    {
        public RemoveCategoryCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public class Handler : IRequestHandler<RemoveCategoryCommand>
        {
            private readonly IBlogDbContext _context;
            private readonly IMediator _mediator;
            public Handler(IBlogDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
                if (category == null) return Unit.Value;
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
