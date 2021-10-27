using MediatR;
using PixelPlus.Application.Blog.Enums;
using PixelPlus.Application.Interfaces;
using PixelPlus.Application.User.Enums;
using PixelPlus.Application.User.Queries;
using PixelPlus.Domain.Exceptions;
using PixelPlus.Domain.User.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PixelPlus.Application.User.Commands
{
    public class AuthenticateCommand : IRequest<UserSummary>
    {
        public AuthenticateCommand(string userName, string password, string jwtKey)
        {
            UserName = userName;
            Password = password;
            JwtKey = jwtKey;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string JwtKey { get; set; }

        public class Handler : IRequestHandler<AuthenticateCommand, UserSummary>
        {
            private readonly IMediator _mediator;
            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<UserSummary> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
            {
                var user = await _mediator.Send(new UserByUserNameQuery(request.UserName), cancellationToken);
                var password = user.GeneratePassword(request.Password);

                if (password != user.Password)
                    throw new BusinessException(UserApplicationException.UserNameOrPasswordWrong,
                        new KeyValuePair<string, string>("userName", request.UserName),
                        new KeyValuePair<string, string>("password", request.Password));

                var userSummary = user.Authenticate(request.JwtKey);
                return userSummary;
            }
        }
    }
}
