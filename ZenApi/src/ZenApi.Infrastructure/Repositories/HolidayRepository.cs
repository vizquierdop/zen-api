using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Holidays;
using ZenApi.Application.Extensions;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfigurationProvider _mapper;

        public HolidayRepository(ApplicationDbContext context, IConfigurationProvider mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<HolidayDto>> SearchPagedAsync(HolidaySearchModel search, CancellationToken cancellationToken)
        {
            var query = _context.Holidays
                .AsNoTracking()
                .AsQueryable();

            query = Application.Models.Holidays.Queries.GetAll.HolidayQueryFilters.CreateFilters(query, search);

            var orderBy = string.IsNullOrEmpty(search.OrderBy) ? "StartDate" : search.OrderBy;

            if (search.OrderDirection == DtOrderDir.Desc)
            {
                query = query.OrderByDescending(e => EF.Property<object>(e, orderBy));
            } else
            {
                query = query.OrderBy(e => EF.Property<object>(e, orderBy));
            }

            var count = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((search.PaginationSkip.GetValueOrDefault(1) - 1) * search.PaginationLength.GetValueOrDefault(10))
                .Take(search.PaginationLength.GetValueOrDefault(10))
                .ProjectTo<HolidayDto>(_mapper)
                .ToListAsync();

            return new PaginatedList<HolidayDto>(items, count, search.PaginationSkip.GetValueOrDefault(1), search.PaginationLength.GetValueOrDefault(10));
        }
    }
}
