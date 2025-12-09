using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IAvailabilityCommandRepository
    {
        Task<Availability?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(Availability entity, CancellationToken cancellationToken);
        Task<List<Availability>> GetByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
        Task UpdateRangeAsync(IEnumerable<Availability> entities, CancellationToken cancellationToken);
    }
}
