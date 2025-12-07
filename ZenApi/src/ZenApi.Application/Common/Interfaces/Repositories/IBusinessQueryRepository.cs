using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Businesses;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IBusinessQueryRepository
    {
        Task<PaginatedList<BusinessDto>> GetAllAsync(BusinessSearchModel search, CancellationToken cancellationToken);
        Task<BusinessDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    }
}
