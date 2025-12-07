using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Interfaces.Repositories;
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
        private readonly IBusinessQueryRepository _businessQueryRepository;
        private readonly ICategoryQueryRepository _categoryQueryRepository;
        private readonly IBusinessCategoryQueryRepository _businessCategoryQueryRepository;
        private readonly IBusinessCategoryCommandRepository _businessCategoryCommandRepository;

        public CreateBusinessCategoryCommandHandler(
            IBusinessQueryRepository businessQueryRepository,
            ICategoryQueryRepository categoryQueryRepository,
            IBusinessCategoryQueryRepository businessCategoryQueryRepository,
            IBusinessCategoryCommandRepository businessCategoryCommandRepository)
        {
            _businessQueryRepository = businessQueryRepository;
            _categoryQueryRepository = categoryQueryRepository;
            _businessCategoryQueryRepository = businessCategoryQueryRepository;
            _businessCategoryCommandRepository = businessCategoryCommandRepository;
        }

        public async Task Handle(CreateBusinessCategoryCommand request, CancellationToken cancellationToken)
        {
            // Check business exists
            var businessExists = await _businessQueryRepository.ExistsAsync(request.BusinessId, cancellationToken);
            if (!businessExists)
                throw new Exception("Business not found");

            // Check category exists
            var categoryExists = await _categoryQueryRepository.ExistsAsync(request.CategoryId, cancellationToken);
            if (!categoryExists)
                throw new Exception("Category not found");

            // Check an existing relation
            var existsRelation =
                await _businessCategoryQueryRepository.ExistsAsync(request.BusinessId, request.CategoryId, cancellationToken);

            if (existsRelation)
                throw new InvalidOperationException("This business is already linked to this category.");

            // Create entity
            var entity = new BusinessCategory
            {
                BusinessId = request.BusinessId,
                CategoryId = request.CategoryId
            };

            await _businessCategoryCommandRepository.CreateAsync(entity, cancellationToken);
        }
    }
}
