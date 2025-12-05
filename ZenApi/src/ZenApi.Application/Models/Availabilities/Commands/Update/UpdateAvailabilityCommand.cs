using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Mappings;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.Availabilities.Commands.Update
{
    public record UpdateAvailabilityCommand : IRequest, IMapTo<Availability>
    {
        public required int Id { get; init; }
        public required bool IsActive { get; init; }
        public string? Slot1Start { get; init; }
        public string? Slot1End { get; init; }
        public string? Slot2Start { get; init; }
        public string? Slot2End { get; init; }
    }

    public class UpdateAvailabilityCommandHandler : IRequestHandler<UpdateAvailabilityCommand>
    {
        private readonly IAvailabilityCommandRepository _repository;
        private readonly IMapper _mapper;

        public UpdateAvailabilityCommandHandler(IAvailabilityCommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new Exception("Availability not found");
            }
            
            _mapper.Map(request, entity);

            await _repository.UpdateAsync(entity, cancellationToken);

            //var entity = await _context.Availabilities
            //    .FindAsync(request.Id, cancellationToken);

            //if (entity is null)
            //    throw new Exception("Availability not found");

            //_mapper.Map(request, entity);

            //await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
