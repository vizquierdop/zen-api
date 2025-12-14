using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.OfferedServices;
using ZenApi.Application.Models.OfferedServices.Commands.Create;
using ZenApi.Application.Models.OfferedServices.Commands.Delete;
using ZenApi.Application.Models.OfferedServices.Commands.Update;
using ZenApi.Application.Models.OfferedServices.Queries.GetAll;
using ZenApi.Application.Models.OfferedServices.Queries.GetSingle;
using ZenApi.Domain.Enums;

namespace ZenApi.API.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class OfferedServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfferedServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all offered services
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<OfferedServiceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] OfferedServiceSearchModel query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOfferedServicesQuery(query), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Returns an offered service by its ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(OfferedServiceDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSingleOfferedServiceQuery(id), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new offered service
        /// </summary>
        [HttpPost]
        [Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<int> Create(
            [FromBody] CreateOfferedServiceCommand command,
            CancellationToken cancellationToken)
        {
            var createdId = await _mediator.Send(command, cancellationToken);
            return createdId;
        }

        /// <summary>
        /// Updates an offered service
        /// </summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateOfferedServiceCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("The route ID and command ID do not match.");

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Deletes an offered service
        /// </summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteOfferedServiceCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
