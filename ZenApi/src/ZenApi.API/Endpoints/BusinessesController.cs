using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Businesses;
using ZenApi.Application.Models.Businesses.Commands.Update;
using ZenApi.Application.Models.Businesses.Queries.GetAll;
using ZenApi.Application.Models.Businesses.Queries.GetSingle;
using ZenApi.Domain.Enums;

namespace ZenApi.API.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class BusinessesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessesController(IMediator mediator)
        { 
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all businesses
        /// </summary>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(PaginatedList<BusinessDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] BusinessSearchModel query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBusinessesQuery(query), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Returns a business by its ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BusinessDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSingleBusinessQuery(id), cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Updates a business
        /// </summary>
        [HttpPut("{id:int}")]
        //[Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateBusinessCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("The ID in the URL must match the ID in the body.");

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
