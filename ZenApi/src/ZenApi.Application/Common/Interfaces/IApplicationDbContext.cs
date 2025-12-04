using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Province> Provinces { get; }
        DbSet<Business> Businesses { get; }
        DbSet<Availability> Availabilities { get; }
        DbSet<Category> Categories { get; }
        DbSet<Holiday> Holidays { get; }
        DbSet<OfferedService> OfferedServices { get; }
        DbSet<Reservation> Reservations { get; }

        DbSet<BusinessCategory> BusinessCategories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
