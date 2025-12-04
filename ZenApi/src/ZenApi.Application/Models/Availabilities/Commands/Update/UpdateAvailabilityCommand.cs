using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Mappings;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.Availabilities.Commands.Update
{
    public class UpdateAvailabilityCommand : IRequest, IMapTo<Availability>
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
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAvailabilityCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Availabilities
                .FindAsync(request.Id, cancellationToken);

            if (entity is null)
                throw new Exception("Availability not found");

            //availability.IsActive = request.IsActive;

            //availability.Slot1Start = request.Slot1Start;
            //availability.Slot1End = request.Slot1End;
            //availability.Slot2Start = request.Slot2Start;
            //availability.Slot2End = request.Slot2End;

            _mapper.Map(request, entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
