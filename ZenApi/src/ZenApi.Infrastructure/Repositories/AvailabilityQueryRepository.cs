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
using ZenApi.Application.Dtos.Availabilities;
using ZenApi.Application.Extensions;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class AvailabilityQueryRepository : IAvailabilityQueryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfigurationProvider _mapper;

        public AvailabilityQueryRepository(ApplicationDbContext context, IConfigurationProvider mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<AvailabilityDto>> GetAllAsync(AvailabilitySearchModel search, CancellationToken cancellationToken)
        {
            var query = _context.Availabilities
                .AsNoTracking()
                .AsQueryable();

            query = Application.Models.Availabilities.Queries.GetAll.AvailabilityQueryFilters.CreateFilters(query, search);

            var orderBy = string.IsNullOrEmpty(search.OrderBy) ? "DayOfWeek" : search.OrderBy;

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
                .ProjectTo<AvailabilityDto>(_mapper)
                .ToListAsync();

            return new PaginatedList<AvailabilityDto>(items, count, search.PaginationSkip.GetValueOrDefault(1), search.PaginationLength.GetValueOrDefault(10));
        }
    }
}
