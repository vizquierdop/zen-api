using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Dtos.Availabilities;

namespace ZenApi.Application.Models.Availabilities.Commands.BulkUpdate
{
    public record BulkUpdateAvailabilitiesCommand : IRequest
    {
        public required int BusinessId { get; init; }
        public required List<BulkUpdateAvailabilitiesDto> Availabilities { get; init; }
    }

    public class BulkUpdateAvailabilitiesCommandHandler : IRequestHandler<BulkUpdateAvailabilitiesCommand>
    {
        private readonly IAvailabilityCommandRepository _repository;
        private readonly IMapper _mapper;

        public BulkUpdateAvailabilitiesCommandHandler(IAvailabilityCommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(BulkUpdateAvailabilitiesCommand request, CancellationToken cancellationToken)
        {
            var currentAvailabilities = await _repository.GetByBusinessIdAsync(request.BusinessId, cancellationToken);

            if (currentAvailabilities == null || !currentAvailabilities.Any())
            {
                throw new Exception($"No availabilities found for Business ID {request.BusinessId}");
            }

            foreach (var incomingDto in request.Availabilities)
            {
                var entityToUpdate = currentAvailabilities.FirstOrDefault(x => x.Id == incomingDto.Id);

                if (entityToUpdate != null)
                {
                    if (entityToUpdate.BusinessId != request.BusinessId) continue;

                    entityToUpdate.IsActive = incomingDto.IsActive;
                    entityToUpdate.Slot1Start = incomingDto.Slot1Start;
                    entityToUpdate.Slot1End = incomingDto.Slot1End;
                    entityToUpdate.Slot2Start = incomingDto.Slot2Start;
                    entityToUpdate.Slot2End = incomingDto.Slot2End;
                }
            }
            await _repository.UpdateRangeAsync(currentAvailabilities, cancellationToken);
        }
    }
}
