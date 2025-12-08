using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZenApi.Application.Models.BusinessCategories.Commands.Create;
using ZenApi.Application.Models.BusinessCategories.Commands.Delete;
using ZenApi.Domain.Enums;

namespace ZenApi.API.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BusinessCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new business-category relation
        /// </summary>
        [HttpPost]
        [Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        public async Task<IActionResult> Create(
            [FromBody] CreateBusinessCategoryCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Deletes a business-category relation
        /// </summary>
        [HttpDelete]
        [Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        public async Task<IActionResult> Delete(
            [FromBody] DeleteBusinessCategoryCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
