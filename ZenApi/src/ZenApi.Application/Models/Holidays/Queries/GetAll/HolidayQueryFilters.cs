using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.Holidays.Queries.GetAll
{
    public static class HolidayQueryFilters
    {
        public static IQueryable<Holiday> CreateFilters(IQueryable<Holiday> query, HolidaySearchModel request)
        {
            query = query.Where(x => x.BusinessId.Equals(request.BusinessId));

            if (request.StartDate.HasValue)
            {
                query = query.Where(x => x.StartDate >= request.StartDate.Value);
            }
            if (request.EndDate.HasValue)
            {
                query = query.Where(x => x.EndDate <= request.EndDate.Value);
            }

            return query;
        }
    }
}
