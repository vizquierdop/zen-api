using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Reservations;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IReservationQueryRepository
    {
        Task<PaginatedList<ReservationDto>> GetAllAsync(ReservationSearchModel search, CancellationToken cancellationToken);
        Task<ReservationDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
