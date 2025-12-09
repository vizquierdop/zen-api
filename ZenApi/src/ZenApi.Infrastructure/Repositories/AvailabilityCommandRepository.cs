using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Domain.Entities;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class AvailabilityCommandRepository : IAvailabilityCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public AvailabilityCommandRepository(ApplicationDbContext context, IConfigurationProvider mapper)
        {
            _context = context;
        }

        public async Task<List<Availability>> GetByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        {
            return await _context.Availabilities
                .Where(x => x.BusinessId == businessId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Availability?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Availabilities
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Availability entity, CancellationToken cancellationToken)
        {
            _context.Availabilities.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRangeAsync(IEnumerable<Availability> entities, CancellationToken cancellationToken)
        {
            _context.Availabilities.UpdateRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
