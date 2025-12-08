using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ZenApi.API.Infrastructure;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Provinces;
using ZenApi.Application.Models.Provinces.Queries.GetAll;

namespace ZenApi.API.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProvincesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProvincesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all provinces.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] ProvinceSearchModel query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProvincesQuery(query), cancellationToken);
            return Ok(result);
        }
    }
}
