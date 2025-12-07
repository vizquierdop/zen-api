using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Categories;
using ZenApi.Application.Extensions;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class CategoryQueryRepository : ICategoryQueryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfigurationProvider _mapper;

        public CategoryQueryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<PaginatedList<CategoryDto>> GetAllAsync(CategorySearchModel search, CancellationToken cancellationToken)
        {
            var query = _context.Categories
                .AsNoTracking()
                .AsQueryable();

            query = Application.Models.Categories.Queries.GetAll.CategoryQueryFilters.CreateFilters(query, search);

            var orderBy = string.IsNullOrEmpty(search.OrderBy) ? "Name" : search.OrderBy;

            if (search.OrderDirection == DtOrderDir.Desc)
            {
                query = query.OrderByDescending(e => EF.Property<object>(e, orderBy));
            }
            else
            {
                query = query.OrderBy(e => EF.Property<object>(e, orderBy));
            }

            var count = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((search.PaginationSkip.GetValueOrDefault(1) - 1) * search.PaginationLength.GetValueOrDefault(10))
                .Take(search.PaginationLength.GetValueOrDefault(10))
                .ProjectTo<CategoryDto>(_mapper)
                .ToListAsync(cancellationToken);

            return new PaginatedList<CategoryDto>(items, count, search.PaginationSkip.GetValueOrDefault(1), search.PaginationLength.GetValueOrDefault(10));
        }

        public async Task<List<int>> GetValidIdsAsync(IEnumerable<int> categoryIds, CancellationToken cancellationToken)
        {
            var ids = categoryIds?.ToArray() ?? Array.Empty<int>();
            if (ids.Length == 0) return new List<int>();

            return await _context.Categories
                .Where(c => ids.Contains(c.Id))
                .Select(c => c.Id)
                .ToListAsync(cancellationToken);
        }

        public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return _context.Categories.AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}
