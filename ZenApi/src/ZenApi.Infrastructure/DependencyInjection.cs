using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Infrastructure.Repositories;
using ZenApi.Infrastructure.Services;

namespace ZenApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ISecurityService, SecurityService>();

            // Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            services.AddScoped<IAvailabilityQueryRepository, AvailabilityQueryRepository>();
            services.AddScoped<IOfferedServiceCommandRepository, OfferedServiceCommandRepository>();
            services.AddScoped<IOfferedServiceQueryRepository, OfferedServiceQueryRepository>();

            return services;
        }
    }
}
