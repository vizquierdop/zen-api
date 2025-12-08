using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Mappings;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.OfferedServices.Commands.Create
{
    public record CreateOfferedServiceCommand : IRequest<int>, IMapTo<OfferedService>
    {
        public required string Name { get; init; }
        public string? Description { get; init; }
        public int? Duration { get; init; }
        public required decimal Price { get; init; }
        public required int BusinessId { get; init; }



        public class CreateOfferedServiceCommandHandler : IRequestHandler<CreateOfferedServiceCommand, int>
        {
            private readonly IOfferedServiceCommandRepository _repository;
            private readonly IMapper _mapper;

            public CreateOfferedServiceCommandHandler(IOfferedServiceCommandRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateOfferedServiceCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<OfferedService>(request);
                entity.IsActive = true;

                if (request.Duration is null)
                {
                    entity.Duration = 0;
                }

                return await _repository.CreateAsync(entity, cancellationToken);
            }
        }
    }
}
