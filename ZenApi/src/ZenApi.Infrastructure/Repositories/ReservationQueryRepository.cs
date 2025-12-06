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
using ZenApi.Application.Dtos.Reservations;
using ZenApi.Application.Extensions;
using ZenApi.Application.Models.Reservations.Queries.GetAll;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class ReservationQueryRepository : IReservationQueryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfigurationProvider _mapper;

        public ReservationQueryRepository(ApplicationDbContext context, IConfigurationProvider mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ReservationDto>> GetAllAsync(ReservationSearchModel search, CancellationToken cancellationToken)
        {
            var query = _context.Reservations
                .AsNoTracking()
                .AsQueryable();

            query = ReservationQueryFilters.CreateFilters(query, search);

            var orderBy = string.IsNullOrEmpty(search.OrderBy) ? "StartDate" : search.OrderBy;

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
                .Include(x => x.Service)
                .Skip((search.PaginationSkip.GetValueOrDefault(1) - 1) * search.PaginationLength.GetValueOrDefault(10))
                .Take(search.PaginationLength.GetValueOrDefault(10))
                .ProjectTo<ReservationDto>(_mapper)
                .ToListAsync(cancellationToken);

            return new PaginatedList<ReservationDto>(items, count, search.PaginationSkip.GetValueOrDefault(1), search.PaginationLength.GetValueOrDefault(10));
        }

        public async Task<ReservationDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Include(x => x.Service)
                .Where(x => x.Id == id)
                .ProjectTo<ReservationDto>(_mapper)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
