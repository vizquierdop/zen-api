using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Dtos.OfferedServices;

namespace ZenApi.Application.Models.OfferedServices.Queries.GetSingle
{
    public record GetSingleOfferedServiceQuery(int Id) : IRequest<OfferedServiceDto>;

    public class GetSingleOfferedServiceQueryHandler : IRequestHandler<GetSingleOfferedServiceQuery, OfferedServiceDto>
    {
        private readonly IOfferedServiceQueryRepository _repository;

        public GetSingleOfferedServiceQueryHandler(IOfferedServiceQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<OfferedServiceDto> Handle(GetSingleOfferedServiceQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (dto == null)
                throw new KeyNotFoundException($"OfferedService with Id {request.Id} not found.");

            return dto;
        }
    }
}
