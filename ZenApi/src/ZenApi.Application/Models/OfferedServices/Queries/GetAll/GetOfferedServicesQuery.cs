using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.OfferedServices;

namespace ZenApi.Application.Models.OfferedServices.Queries.GetAll
{
    public record GetOfferedServicesQuery : OfferedServiceSearchModel, IRequest<PaginatedList<OfferedServiceDto>>;

    public class GetOfferedServicesQueryHandler : IRequestHandler<GetOfferedServicesQuery, PaginatedList<OfferedServiceDto>>
    {
        private readonly IOfferedServiceQueryRepository _repository;

        public GetOfferedServicesQueryHandler(IOfferedServiceQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<OfferedServiceDto>> Handle(GetOfferedServicesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(request, cancellationToken);
        }
    }
}
