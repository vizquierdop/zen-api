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
using ZenApi.Domain.Entities;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class OfferedServiceCommandRepository : IOfferedServiceCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public OfferedServiceCommandRepository(ApplicationDbContext context, IConfigurationProvider mapper)
        {
            _context = context;
        }

        public async Task<OfferedService?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.OfferedServices
            .Include(x => x.Business)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<int> CreateAsync(OfferedService entity, CancellationToken cancellationToken)
        {
            await _context.OfferedServices.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task UpdateAsync(OfferedService entity, CancellationToken cancellationToken)
        {
            _context.OfferedServices.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _context.OfferedServices
                .FindAsync(new object[] { id }, cancellationToken);

            if (entity is null)
                throw new Exception("Offered Service not found");

            _context.OfferedServices.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
