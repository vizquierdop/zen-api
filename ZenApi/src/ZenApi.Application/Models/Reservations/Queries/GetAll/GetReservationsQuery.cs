using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Reservations;

namespace ZenApi.Application.Models.Reservations.Queries.GetAll
{
    public record GetReservationsQuery : ReservationSearchModel, IRequest<PaginatedList<ReservationDto>>;

    public class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, PaginatedList<ReservationDto>>
    {
        private readonly IReservationQueryRepository _repository;

        public GetReservationsQueryHandler(IReservationQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<ReservationDto>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(request, cancellationToken);
        }
    }
}
