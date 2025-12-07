using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZenApi.Application.Models.Users.Commands.Create;
using ZenApi.Application.Models.Users.Commands.Update;
using ZenApi.Application.Models.Users.Queries.GetSingle;

namespace ZenApi.API.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns a user by its ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSingleUserQuery(id), cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        [HttpPost]
        public async Task<int> Create(
            [FromBody] CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            var createdId = await _mediator.Send(command, cancellationToken);
            return createdId;
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateUserCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("The route ID and command ID do not match.");

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
