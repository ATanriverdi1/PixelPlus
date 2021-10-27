using MediatR;
using Microsoft.EntityFrameworkCore;
using PixelPlus.Application.Interfaces;
using PixelPlus.Application.User.Enums;
using PixelPlus.Domain.Exceptions;
using PixelPlus.Domain.User;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.User.Commands
{
    public class CreateUserCommand : IRequest
    {
        public CreateUserCommand(string firstName, string lastName, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = password;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<CreateUserCommand>
        {
            private readonly IBlogDbContext _context;

            public Handler(IBlogDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var userCheck = await _context.Users.FirstOrDefaultAsync(p => p.UserName == request.UserName, cancellationToken);
                
                if (userCheck != null)
                    throw new BusinessException(UserApplicationException.UserNameAlreadyExist,
                        new KeyValuePair<string, string>("userName", request.UserName));

                var user = new UserAggregate(request.FirstName, request.LastName, request.UserName, request.Password);
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
