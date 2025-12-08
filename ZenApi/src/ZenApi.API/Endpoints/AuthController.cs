using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZenApi.Application.Dtos.Auth.RefreshToken;
using ZenApi.Application.Models.Auth.Login;
using ZenApi.Application.Models.Auth.RefreshToken;
using ZenApi.Application.Models.Auth.RevokeToken;

namespace ZenApi.API.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
        {
            var command = new RefreshTokenCommand(request.Token);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto request)
        {
            var command = new RevokeTokenCommand(request.Token);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
