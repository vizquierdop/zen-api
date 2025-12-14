using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Availabilities;
using ZenApi.Application.Models.Availabilities.Commands.BulkUpdate;
using ZenApi.Application.Models.Availabilities.Commands.Update;
using ZenApi.Application.Models.Availabilities.Queries.GetAll;
using ZenApi.Domain.Enums;

namespace ZenApi.API.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class AvailabilitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AvailabilitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all availabilities of a business
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<AvailabilityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] AvailabilitySearchModel query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAvailabilitiesQuery(query), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Update an availability
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateAvailabilityCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Id in URL does not match Id in body.");

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Bulk update all availabilities for a specific business
        /// </summary>
        [HttpPut("business/{businessId}")]
        [Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBulk(
            int businessId,
            [FromBody] BulkUpdateAvailabilitiesCommand command,
            CancellationToken cancellationToken)
        {
            if (businessId != command.BusinessId)
                return BadRequest("BusinessId in URL does not match BusinessId in body.");

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
