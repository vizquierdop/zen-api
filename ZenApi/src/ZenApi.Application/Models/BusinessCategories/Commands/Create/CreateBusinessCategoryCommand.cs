using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.BusinessCategories.Commands.Create
{
    public record CreateBusinessCategoryCommand : IRequest
    {
        public required int BusinessId { get; init; }
        public required int CategoryId { get; init; }
    }

    public class CreateBusinessCategoryCommandHandler
        : IRequestHandler<CreateBusinessCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateBusinessCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateBusinessCategoryCommand request, CancellationToken cancellationToken)
        {
            // Check business exists
            var businessExists = await _context.Businesses
                .AnyAsync(x => x.Id == request.BusinessId, cancellationToken);

            if (!businessExists)
                throw new Exception("Business not found");

            // Check category exists
            var categoryExists = await _context.Categories
                .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

            if (!categoryExists)
                throw new Exception("Category not found");

            // Prevent already linked registers
            var existsRelation = await _context.BusinessCategories
                .AnyAsync(
                    x => x.BusinessId == request.BusinessId && x.CategoryId == request.CategoryId,
                    cancellationToken);

            if (existsRelation)
                throw new InvalidOperationException("This business is already linked to this category.");

            var entity = new BusinessCategory
            {
                BusinessId = request.BusinessId,
                CategoryId = request.CategoryId
            };

            _context.BusinessCategories.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
