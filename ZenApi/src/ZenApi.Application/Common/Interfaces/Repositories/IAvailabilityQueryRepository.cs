using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Availabilities;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IAvailabilityQueryRepository
    {
        Task<PaginatedList<AvailabilityDto>> GetAllAsync(AvailabilitySearchModel search, CancellationToken cancellationToken);
    }
}
