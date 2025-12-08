using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Models.Holidays.Commands.Create;
using ZenApi.Application.Models.Holidays.Commands.Delete;
using ZenApi.Application.Models.Holidays.Queries.GetAll;
using ZenApi.Domain.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZenApi.API.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HolidaysController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HolidaysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all holidays of a business
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] HolidaySearchModel query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetHolidaysQuery(query), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new holiday
        /// </summary>
        [HttpPost]
        [Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        public async Task<int> Create(
            [FromBody] CreateHolidayCommand command,
            CancellationToken cancellationToken)
        {
            var createdId = await _mediator.Send(command, cancellationToken);
            return createdId;
        }

        /// <summary>
        /// Deletes a holiday
        /// </summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteHolidayCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
