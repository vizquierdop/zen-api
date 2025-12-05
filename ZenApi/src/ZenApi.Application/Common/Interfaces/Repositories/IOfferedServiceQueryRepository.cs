using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.OfferedServices;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IOfferedServiceQueryRepository
    {
        Task<PaginatedList<OfferedServiceDto>> GetAllAsync(OfferedServiceSearchModel search, CancellationToken cancellationToken);
        Task<OfferedServiceDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
