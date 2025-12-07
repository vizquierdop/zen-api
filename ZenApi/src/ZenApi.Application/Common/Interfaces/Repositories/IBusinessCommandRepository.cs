using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IBusinessCommandRepository
    {
        Task<int> CreateAsync(Business entity, CancellationToken cancellationToken);
        Task<Business?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(Business entity, CancellationToken cancellationToken);
    }
}
