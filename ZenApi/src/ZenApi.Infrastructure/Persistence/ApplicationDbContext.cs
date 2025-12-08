using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ZenApi.Domain.Common;
using ZenApi.Domain.Entities;
using ZenApi.Infrastructure.Identity;

namespace ZenApi.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> UsersDomain => Set<User>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<Business> Businesses => Set<Business>();
    public DbSet<Availability> Availabilities => Set<Availability>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Holiday> Holidays => Set<Holiday>();
    public DbSet<OfferedService> OfferedServices => Set<OfferedService>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    public DbSet<BusinessCategory> BusinessCategories => Set<BusinessCategory>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Created = DateTimeOffset.UtcNow;
                entry.Entity.CreatedBy = "system";
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModified = DateTimeOffset.UtcNow;
                entry.Entity.LastModifiedBy = "system";
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
