using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Models.Categories.Queries.GetAll
{
    public static class CategoryQueryFilters
    {
        public static IQueryable<Category> CreateFilters(IQueryable<Category> query, CategorySearchModel request)
        {
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var name = request.Name.ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(name));
            }

            return query;
        }
    }
}
