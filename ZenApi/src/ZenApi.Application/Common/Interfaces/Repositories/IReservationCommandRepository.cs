using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IReservationCommandRepository
    {
        Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<int> CreateAsync(Reservation entity, CancellationToken cancellationToken);
        Task UpdateAsync(Reservation entity, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
