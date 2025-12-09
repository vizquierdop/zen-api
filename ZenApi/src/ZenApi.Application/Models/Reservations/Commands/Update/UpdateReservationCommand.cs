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

namespace ZenApi.Application.Models.Reservations.Commands.Update
{
    public record UpdateReservationCommand : IRequest, IMapTo<Reservation>
    {
        public required int Id { get; init; }
        public required DateTime Date { get; init; }
        public required string StartTime { get; init; }
        public required string EndTime { get; init; }
        public string? CustomerName { get; init; }
        public string? CustomerEmail { get; init; }
        public string? CustomerPhone { get; init; }
        public required int ServiceId { get; init; }
    }

    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
    {
        private readonly IReservationCommandRepository _repository;
        private readonly IMapper _mapper;

        public UpdateReservationCommandHandler(IReservationCommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null)
                throw new Exception("Reservation not found");

            _mapper.Map(request, entity);

            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }
}
