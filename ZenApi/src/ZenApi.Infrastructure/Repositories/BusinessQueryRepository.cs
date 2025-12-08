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
using ZenApi.Application.Dtos.Businesses;
using ZenApi.Application.Extensions;
using ZenApi.Application.Models.Businesses.Queries.GetAll;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class BusinessQueryRepository : IBusinessQueryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;

        public BusinessQueryRepository(ApplicationDbContext context, IMapper mapper, IConfigurationProvider configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<PaginatedList<BusinessDto>> GetAllAsync(BusinessSearchModel search, CancellationToken cancellationToken)
        {
            var query = _context.Businesses
                .AsNoTracking()
                .AsQueryable();

            query = BusinessQueryFilters.CreateFilters(query, search);

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

            var skip = (search.PaginationSkip.GetValueOrDefault(1) - 1) * search.PaginationLength.GetValueOrDefault(10);
            var take = search.PaginationLength.GetValueOrDefault(10);

            var items = await query
                .ProjectTo<BusinessDto>(_configuration)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);

            return new PaginatedList<BusinessDto>(items, count, search.PaginationSkip.GetValueOrDefault(1), search.PaginationLength.GetValueOrDefault(10));
        }

        public async Task<BusinessDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Businesses
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectTo<BusinessDto>(_configuration)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return _context.Businesses.AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}
