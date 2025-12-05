using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;

namespace ZenApi.Application.Models.OfferedServices.Commands.Delete
{
    public record DeleteOfferedServiceCommand(int Id) : IRequest;

    public class DeleteOfferedServiceCommandHandler : IRequestHandler<DeleteOfferedServiceCommand>
    {
        private readonly IOfferedServiceCommandRepository _repository;

        public DeleteOfferedServiceCommandHandler(IOfferedServiceCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteOfferedServiceCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
