using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.OfferedServices;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IOfferedServiceCommandRepository
    {
        Task<OfferedService?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<int> CreateAsync(OfferedService entity, CancellationToken cancellationToken);
        Task UpdateAsync(OfferedService entity, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
