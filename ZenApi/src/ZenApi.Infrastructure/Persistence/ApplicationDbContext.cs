using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
}
