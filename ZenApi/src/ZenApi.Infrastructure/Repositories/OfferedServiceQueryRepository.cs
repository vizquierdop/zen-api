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
using ZenApi.Application.Dtos.OfferedServices;
using ZenApi.Application.Extensions;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class OfferedServiceQueryRepository : IOfferedServiceQueryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfigurationProvider _mapper;

        public OfferedServiceQueryRepository(ApplicationDbContext context, IConfigurationProvider mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<OfferedServiceDto>> GetAllAsync(OfferedServiceSearchModel search, CancellationToken cancellationToken)
        {
            var query = _context.OfferedServices
                .AsNoTracking()
                .AsQueryable();

            query = Application.Models.OfferedServices.Queries.GetAll.OfferedServiceQueryFilters.CreateFilters(query, search);

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
                .Include(x => x.Business)
                .Skip((search.PaginationSkip.GetValueOrDefault(1) - 1) * search.PaginationLength.GetValueOrDefault(10))
                .Take(search.PaginationLength.GetValueOrDefault(10))
                .ProjectTo<OfferedServiceDto>(_mapper)
                .ToListAsync();

            return new PaginatedList<OfferedServiceDto>(items, count, search.PaginationSkip.GetValueOrDefault(1), search.PaginationLength.GetValueOrDefault(10));
        }

        public async Task<OfferedServiceDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.OfferedServices
                .AsNoTracking()
                .Include(x => x.Business)
                .Where(x => x.Id == id)
                .ProjectTo<OfferedServiceDto>(_mapper)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
