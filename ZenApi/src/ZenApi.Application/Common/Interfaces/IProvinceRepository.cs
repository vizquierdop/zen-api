using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Provinces;

namespace ZenApi.Application.Common.Interfaces
{
    public interface IProvinceRepository
    {
        Task<PaginatedList<ProvinceDto>> SearchPagedAsync(ProvinceSearchModel search, CancellationToken cancellationToken);
    }
}
