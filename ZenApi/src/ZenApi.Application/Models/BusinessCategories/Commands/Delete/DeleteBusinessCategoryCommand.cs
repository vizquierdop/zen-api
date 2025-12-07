using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Interfaces.Repositories;

namespace ZenApi.Application.Models.BusinessCategories.Commands.Delete
{
    public record DeleteBusinessCategoryCommand : IRequest
    {
        public required int BusinessId { get; init; }
        public required int CategoryId { get; init; }
    }

    public class DeleteBusinessCategoryCommandHandler
        : IRequestHandler<DeleteBusinessCategoryCommand>
    {
        private readonly IBusinessCategoryCommandRepository _repository;

        public DeleteBusinessCategoryCommandHandler(IBusinessCategoryCommandRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteBusinessCategoryCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.CategoryId, request.BusinessId, cancellationToken);
        }
    }
}
