using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Interfaces.Repositories;

namespace ZenApi.Application.Models.Reservations.Commands.Delete
{
    public record DeleteReservationCommand(int Id) : IRequest;

    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
    {
        private readonly IReservationCommandRepository _repository;

        public DeleteReservationCommandHandler(IReservationCommandRepository repository) { _repository = repository; }

        public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
