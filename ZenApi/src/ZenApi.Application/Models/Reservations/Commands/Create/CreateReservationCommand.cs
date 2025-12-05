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
using ZenApi.Domain.Enums;

namespace ZenApi.Application.Models.Reservations.Commands.Create
{
    public record CreateReservationCommand : IRequest<int>, IMapTo<Reservation>
    {
        public required DateTime Date { get; init; }
        public required string StartTime { get; init; }
        public required string EndTime { get; init; }
        public string? CustomerName { get; init; }
        public string? CustomerEmail { get; init; }
        public string? CustomerPhone { get; init; }

        // Relations
        public required int ServiceId { get; init; }
        public int? UserId { get; init; }
    }

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
    {
        private readonly IReservationCommandRepository _repository;
        private readonly IMapper _mapper;

        public CreateReservationCommandHandler(IReservationCommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Reservation>(request);

            entity.Status = ReservationStatus.Pending;

            return await _repository.CreateAsync(entity, cancellationToken);
        }
    }
}
