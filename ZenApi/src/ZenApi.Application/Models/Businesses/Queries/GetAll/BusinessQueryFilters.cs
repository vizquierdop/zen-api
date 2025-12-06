using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.Businesses.Queries.GetAll
{
    public static class BusinessQueryFilters
    {
        public static IQueryable<Business> CreateFilters(IQueryable<Business> query, BusinessSearchModel request)
        {
            if (request.Name != null)
            {
                query = query.Where(x => x.Name.ToLower() == request.Name.ToLower() || x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(request.CategoryIds))
            {
                var ids = request.CategoryIds
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.TryParse(id, out var value) ? value : (int?)null)
                    .Where(x => x.HasValue)
                    .Select(x => x!.Value)
                    .ToList();

                if (ids.Count > 0)
                {
                    query = query.Where(b =>
                        b.BusinessCategories.Any(bc => ids.Contains(bc.CategoryId)));
                }
            }

            if (request.IsActive != null)
            {
                query = query.Where(x => x.IsActive == request.IsActive);
            }

            return query;
        }
    }
}
