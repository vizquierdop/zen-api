using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.Availabilities.Queries.GetAll
{
    public static class AvailabilityQueryFilters
    {
        public static IQueryable<Availability> CreateFilters(IQueryable<Availability> query, AvailabilitySearchModel request)
        {
            query = query.Where(x => x.BusinessId.Equals(request.BusinessId));

            return query;
        }
    }
}
