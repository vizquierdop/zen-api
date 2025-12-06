using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.OfferedServices.Queries.GetAll
{
    public static class OfferedServiceQueryFilters
    {
        public static IQueryable<OfferedService> CreateFilters(IQueryable<OfferedService> query, OfferedServiceSearchModel request)
        {
            if (request.BusinessId != null)
            {
                query = query.Where(x => x.BusinessId.Equals(request.BusinessId));
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.ToLower().StartsWith(request.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.Name.ToLower().Contains(request.Search.ToLower()));
            }

            if (request.Duration != null)
            {
                query = query.Where(x => x.Duration == request.Duration);
            }

            if (request.Price != null)
            {
                query = query.Where(x => x.Price == request.Price);
            }

            if (request.IsActive != null) 
            { 
                query = query.Where(x=> x.IsActive == request.IsActive);
            }

            if (request.ProvinceId != null)
            {
                query = query.Where(x => x.Business != null && x.Business.ProvinceId == request.ProvinceId);
            }

            return query;
        }
    }
}
