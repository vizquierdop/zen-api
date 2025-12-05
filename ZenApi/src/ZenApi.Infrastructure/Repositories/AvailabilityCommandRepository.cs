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
        private readonly IConfigurationProvider _mapper;

        public AvailabilityCommandRepository(ApplicationDbContext context, IConfigurationProvider mapper)
        {
            _context = context;
            _mapper = mapper;
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
    }
}
