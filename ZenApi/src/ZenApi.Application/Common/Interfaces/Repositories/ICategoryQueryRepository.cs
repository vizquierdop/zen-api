using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Categories;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface ICategoryQueryRepository
    {
        Task<PaginatedList<CategoryDto>> GetAllAsync(CategorySearchModel search, CancellationToken cancellationToken);
    }
}
