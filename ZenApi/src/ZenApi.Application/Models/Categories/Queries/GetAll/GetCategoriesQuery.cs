using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Categories;

namespace ZenApi.Application.Models.Categories.Queries.GetAll
{
    public record GetCategoriesQuery : CategorySearchModel, IRequest<PaginatedList<CategoryDto>>;

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, PaginatedList<CategoryDto>>
    {
        private readonly ICategoryQueryRepository _repository;

        public GetCategoriesQueryHandler(ICategoryQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(request, cancellationToken);
        }
    }
}
