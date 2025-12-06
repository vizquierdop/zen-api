using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Provinces;

namespace ZenApi.Application.Models.Provinces.Queries.GetAll
{
    public record GetProvincesQuery : ProvinceSearchModel, IRequest<PaginatedList<ProvinceDto>>;

    public class GetProvincesQueryHandler : IRequestHandler<GetProvincesQuery, PaginatedList<ProvinceDto>>
    {
        private readonly IProvinceQueryRepository _repository;

        public GetProvincesQueryHandler(IProvinceQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<ProvinceDto>> Handle(GetProvincesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(request, cancellationToken);
        }
    }
}
