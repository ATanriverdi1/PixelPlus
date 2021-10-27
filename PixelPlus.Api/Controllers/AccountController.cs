using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PixelPlus.Api.Models.Account.Requests;
using PixelPlus.Application.User.Commands;
using PixelPlus.Domain.User.ValueObject;
using System.Threading.Tasks;

namespace PixelPlus.Api.Controllers
{
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(UserSummary), 201)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            return Ok(await _mediator.Send(request.ToCommand(_configuration["JWT:key"])));
        }

        [HttpPost("register")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
