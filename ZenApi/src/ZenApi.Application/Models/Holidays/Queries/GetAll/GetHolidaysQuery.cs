using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Holidays;

namespace ZenApi.Application.Models.Holidays.Queries.GetAll
{
    public record GetHolidaysQuery : HolidaySearchModel, IRequest<PaginatedList<HolidayDto>>;

    public class GetHolidaysQueryHandler : IRequestHandler<GetHolidaysQuery, PaginatedList<HolidayDto>>
    {
        private readonly IHolidayRepository _repository;

        public GetHolidaysQueryHandler(IHolidayRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<HolidayDto>> Handle(GetHolidaysQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SearchPagedAsync(request, cancellationToken);
        }
    }
}
