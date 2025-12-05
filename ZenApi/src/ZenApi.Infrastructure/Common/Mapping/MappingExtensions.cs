using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Models;
using ZenApi.Infrastructure.Common.Pagination;

namespace ZenApi.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
            this IQueryable source,
            IConfigurationProvider configuration)
            where TDestination : class
        {
            return source
                .ProjectTo<TDestination>(configuration)
                .AsNoTracking()
                .ToListAsync();
        }

        public static Task<PaginatedList<TDestination>> ProjectToPaginatedListAsync<TDestination>(
            this IQueryable source,
            IConfigurationProvider configuration,
            int pageNumber,
            int pageSize)
            where TDestination : class
        {
            return source
                .ProjectTo<TDestination>(configuration)
                .AsNoTracking()
                .ToPaginatedListAsync(pageNumber, pageSize);
        }
    }
}
