using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ZenApi.Domain.Entities;

namespace ZenApi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<Business> Businesses => Set<Business>();
    public DbSet<Availability> Availabilities => Set<Availability>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Holiday> Holidays => Set<Holiday>();
    public DbSet<OfferedService> OfferedServices => Set<OfferedService>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    public DbSet<BusinessCategory> BusinessCategories => Set<BusinessCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
