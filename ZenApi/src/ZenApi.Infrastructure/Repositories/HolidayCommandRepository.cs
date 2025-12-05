using AutoMapper;
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
    public class HolidayCommandRepository : IHolidayCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public HolidayCommandRepository(ApplicationDbContext context, IConfigurationProvider mapper)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Holiday entity, CancellationToken cancellationToken)
        {
            await _context.Holidays.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _context.Holidays
                .FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                throw new Exception("Holiday not found");
            }

            _context.Holidays.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
