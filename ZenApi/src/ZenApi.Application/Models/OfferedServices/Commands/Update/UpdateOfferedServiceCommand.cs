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

namespace ZenApi.Application.Models.OfferedServices.Commands.Update
{
    public record UpdateOfferedServiceCommand : IRequest, IMapTo<OfferedService>
    {
        public required int Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public int? Duration { get; init; }
        public decimal? Price { get; init; }
        public bool? IsActive { get; init; }
    }

    public class UpdateOfferedServiceCommandHandler : IRequestHandler<UpdateOfferedServiceCommand>
    {
        private readonly IOfferedServiceCommandRepository _repository;
        private readonly IMapper _mapper;

        public UpdateOfferedServiceCommandHandler(
            IOfferedServiceCommandRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateOfferedServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null)
                throw new Exception("Offered service not found");

            _mapper.Map(request, entity);

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
