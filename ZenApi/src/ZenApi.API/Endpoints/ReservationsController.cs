using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Models.Reservations.Commands.Create;
using ZenApi.Application.Models.Reservations.Commands.Delete;
using ZenApi.Application.Models.Reservations.Commands.Update;
using ZenApi.Application.Models.Reservations.Queries.GetAll;

namespace ZenApi.API.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all reservations
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] ReservationSearchModel query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetReservationsQuery(query), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new reservation
        /// </summary>
        [HttpPost]
        public async Task<int> Create(
            [FromBody] CreateReservationCommand command,
            CancellationToken cancellationToken)
        {
            var createdId = await _mediator.Send(command, cancellationToken);
            return createdId;
        }

        /// <summary>
        /// Updates a reservation
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateReservationCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("The route ID and command ID do not match.");

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Deletes a reservation
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteReservationCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
