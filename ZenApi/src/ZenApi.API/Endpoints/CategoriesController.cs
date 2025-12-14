using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Categories;
using ZenApi.Application.Models.Categories.Queries.GetAll;

namespace ZenApi.API.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all categories
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] CategorySearchModel query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCategoriesQuery(query), cancellationToken);
            return Ok(result);
        }
    }
}
