using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.Provinces.Queries.GetAll
{
    public static class ProvinceQueryFilters
    {
        public static IQueryable<Province> CreateFilters(IQueryable<Province> query, ProvinceSearchModel request)
        {
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var name = request.Name.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(name));
            }

            return query;
        }
    }
}
