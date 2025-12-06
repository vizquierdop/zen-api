using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Businesses;

namespace ZenApi.Application.Models.Businesses.Queries.GetAll
{
    public record GetBusinessesQuery : BusinessSearchModel, IRequest<PaginatedList<BusinessDto>>;

    public class GetBusinessesQueryHandler : IRequestHandler<GetBusinessesQuery, PaginatedList<BusinessDto>>
    {
        private readonly IBusinessQueryCommand _repository;

        public GetBusinessesQueryHandler(IBusinessQueryCommand repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<BusinessDto>> Handle(GetBusinessesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(request, cancellationToken);
        }
    }
}
