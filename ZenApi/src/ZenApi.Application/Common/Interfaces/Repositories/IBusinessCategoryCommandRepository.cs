using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IBusinessCategoryCommandRepository
    {
        Task CreateAsync(BusinessCategory entity, CancellationToken cancellationToken);
        Task DeleteAsync(int categoryId, int businessId, CancellationToken cancellationToken);
    }
}
