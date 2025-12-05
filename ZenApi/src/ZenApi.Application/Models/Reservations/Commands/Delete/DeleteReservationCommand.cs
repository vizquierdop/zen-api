using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;

namespace ZenApi.Application.Models.Reservations.Commands.Delete
{
    public record DeleteReservationCommand(int Id) : IRequest;

    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteReservationCommandHandler(IApplicationDbContext context) { _context = context; }

        public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Reservations
                .FindAsync(request.Id, cancellationToken);

            if (entity is null)
                throw new Exception("Reservation not found");

            _context.Reservations.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
