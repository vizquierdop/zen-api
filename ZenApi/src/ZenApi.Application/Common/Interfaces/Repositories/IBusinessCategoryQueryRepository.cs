using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IBusinessCategoryQueryRepository
    {
        Task<bool> ExistsAsync(int businessId, int categoryId, CancellationToken cancellationToken);
    }
}
