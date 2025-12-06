using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Availabilities;

namespace ZenApi.Application.Models.Availabilities.Queries.GetAll
{
    public record GetAvailabilitiesQuery : AvailabilitySearchModel, IRequest<PaginatedList<AvailabilityDto>>;

    public class GetAvailabilitiesQueryHandler : IRequestHandler<GetAvailabilitiesQuery, PaginatedList<AvailabilityDto>>
    {
        private readonly IAvailabilityQueryRepository _repository;

        public GetAvailabilitiesQueryHandler(IAvailabilityQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<AvailabilityDto>> Handle(GetAvailabilitiesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(request, cancellationToken);
        }
    }
}
