using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;

namespace ZenApi.Application.Models.OfferedServices.Commands.Delete
{
    public record DeleteOfferedServiceCommand(int Id) : IRequest;

    public class DeleteOfferedServiceCommandHandler : IRequestHandler<DeleteOfferedServiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOfferedServiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteOfferedServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.OfferedServices
                .FindAsync(request.Id, cancellationToken);

            if (entity is null)
                throw new Exception("Offered Service not found");

            _context.OfferedServices.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
