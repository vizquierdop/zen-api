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
    [Produces("application/json")]
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
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        public async Task<IActionResult> Create(
            [FromBody] CreateBusinessCategoryCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Removes a category from a business
        /// </summary>
        [HttpDelete("business/{businessId}/category/{categoryId}")]
        [Authorize(Roles = $"{nameof(UserRole.Business)},{nameof(UserRole.Admin)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int businessId, int categoryId, CancellationToken cancellationToken)
        {
            var command = new DeleteBusinessCategoryCommand
            {
                BusinessId = businessId,
                CategoryId = categoryId
            };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
