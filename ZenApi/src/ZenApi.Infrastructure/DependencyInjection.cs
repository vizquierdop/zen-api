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
            services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
            services.AddScoped<IProvinceQueryRepository, ProvinceQueryRepository>();
            services.AddScoped<IHolidayQueryRepository, HolidayQueryRepository>();
            services.AddScoped<IHolidayCommandRepository, HolidayCommandRepository>();
            services.AddScoped<IAvailabilityQueryRepository, AvailabilityQueryRepository>();
            services.AddScoped<IAvailabilityCommandRepository, AvailabilityCommandRepository>();
            services.AddScoped<IOfferedServiceCommandRepository, OfferedServiceCommandRepository>();
            services.AddScoped<IOfferedServiceQueryRepository, OfferedServiceQueryRepository>();
            services.AddScoped<IReservationCommandRepository, ReservationCommandRepository>();
            services.AddScoped<IReservationQueryRepository, ReservationQueryRepository>();
            services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            services.AddScoped<IUserQueryRepository, UserQueryRepository>();
            services.AddScoped<IBusinessCommandRepository, BusinessCommandRepository>();
            services.AddScoped<IBusinessQueryRepository, BusinessQueryRepository>();
            services.AddScoped<IBusinessCategoryCommandRepository, BusinessCategoryCommandRepository>();
            services.AddScoped<IBusinessCategoryQueryRepository, BusinessCategoryQueryRepository>();

            return services;
        }
    }
}
