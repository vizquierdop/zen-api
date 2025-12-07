using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Dtos.Businesses;

namespace ZenApi.Application.Models.Businesses.Queries.GetSingle
{
    public record GetSingleBusinessQuery(int Id) : IRequest<BusinessDto>;

    public class GetSingleBusinessQueryHandler : IRequestHandler<GetSingleBusinessQuery, BusinessDto>
    {
        private readonly IBusinessQueryRepository _repository;

        public GetSingleBusinessQueryHandler(IBusinessQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<BusinessDto> Handle(GetSingleBusinessQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (dto == null)
                throw new KeyNotFoundException($"Business with Id {request.Id} not found.");

            return dto;
        }
    }
}
