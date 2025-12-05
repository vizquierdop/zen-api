using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Holidays;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IHolidayQueryRepository
    {
        Task<PaginatedList<HolidayDto>> GetAllAsync(HolidaySearchModel search, CancellationToken cancellationToken);
    }
}
