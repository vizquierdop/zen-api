using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZenApi.Application.Models.BusinessCategories.Commands.Create;
using ZenApi.Application.Models.BusinessCategories.Commands.Delete;

namespace ZenApi.API.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Delete(
            [FromBody] DeleteBusinessCategoryCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
