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
    public class BusinessCategoryCommandRepository : IBusinessCategoryCommandRepository
    {
        private readonly ApplicationDbContext _context;

        public BusinessCategoryCommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(BusinessCategory entity, CancellationToken cancellationToken)
        {
            await _context.BusinessCategories.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int categoryId, int businessId, CancellationToken cancellationToken)
        {
            var entity = await _context.BusinessCategories
                .FindAsync(new object[] { businessId, categoryId},
                    cancellationToken);

            if (entity is null)
                throw new Exception("Business-Category not found");

            _context.BusinessCategories.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
