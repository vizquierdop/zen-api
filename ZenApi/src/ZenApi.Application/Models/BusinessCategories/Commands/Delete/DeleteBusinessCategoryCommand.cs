using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;

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
        private readonly IApplicationDbContext _context;

        public DeleteBusinessCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteBusinessCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.BusinessCategories
                .FirstOrDefaultAsync(
                    x => x.BusinessId == request.BusinessId
                      && x.CategoryId == request.CategoryId,
                    cancellationToken);

            if (entity is null)
                throw new Exception("Business-Category relation not found");

            _context.BusinessCategories.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
