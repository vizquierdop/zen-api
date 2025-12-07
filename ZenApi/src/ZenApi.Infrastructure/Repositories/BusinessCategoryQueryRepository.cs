using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class BusinessCategoryQueryRepository : IBusinessCategoryQueryRepository
    {
        private readonly ApplicationDbContext _context;

        public BusinessCategoryQueryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> ExistsAsync(int businessId, int categoryId, CancellationToken cancellationToken)
        {
            return _context.BusinessCategories
                .AnyAsync(x =>
                    x.BusinessId == businessId &&
                    x.CategoryId == categoryId,
                    cancellationToken);
        }
    }
}
