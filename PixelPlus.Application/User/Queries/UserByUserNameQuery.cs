using MediatR;
using Microsoft.EntityFrameworkCore;
using PixelPlus.Application.Exceptions;
using PixelPlus.Application.Interfaces;
using PixelPlus.Application.User.Enums;
using PixelPlus.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.User.Queries
{
    public class UserByUserNameQuery : IRequest<UserAggregate>
    {
        public UserByUserNameQuery(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }

        public class Handler : IRequestHandler<UserByUserNameQuery, UserAggregate>
        {
            private readonly IBlogDbContext _context;

            public Handler(IBlogDbContext context)
            {
                _context = context;
            }

            public async Task<UserAggregate> Handle(UserByUserNameQuery request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(p => p.UserName == request.UserName, cancellationToken);

                if (user == null)
                    throw new NotFoundException(UserApplicationException.UserNotFound,
                        new KeyValuePair<string, string>("userName", request.UserName));

                return user;
            }
        }
    }
}
