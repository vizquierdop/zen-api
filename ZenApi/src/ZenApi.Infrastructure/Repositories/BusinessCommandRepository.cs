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
    public class BusinessCommandRepository : IBusinessCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public BusinessCommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Business entity, CancellationToken cancellationToken)
        {
            await _context.Businesses.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            if (entity.Availabilities == null || entity.Availabilities.Count == 0)
            {
                for (int day = 0; day <= 6; day++)
                {
                    var a = new Availability
                    {
                        DayOfWeek = day,
                        IsActive = false,
                        BusinessId = entity.Id
                    };
                    _context.Availabilities.Add(a);
                }
                await _context.SaveChangesAsync(cancellationToken);
            }

            if (entity.BusinessCategories != null && entity.BusinessCategories.Count > 0)
            {
                foreach (var bc in entity.BusinessCategories)
                {
                    if (bc.BusinessId == 0) bc.BusinessId = entity.Id;
                    _context.BusinessCategories.Add(bc);
                }
                await _context.SaveChangesAsync(cancellationToken);
            }

            return entity.Id;
        }

        public async Task<Business?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Businesses
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Business entity, CancellationToken cancellationToken)
        {
            _context.Businesses.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
