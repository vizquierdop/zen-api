using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZenApi.Domain.Entities;
using ZenApi.Domain.Enums;
using ZenApi.Infrastructure.Identity;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.IntegrationTests.Helpers;

public static class TestDataSeeder
{
    /// <summary>
    /// Seeds a test user in the database
    /// </summary>
    public static async Task<int> SeedTestUserAsync(IServiceProvider serviceProvider, string email, string password, UserRole role = UserRole.Customer)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var appUser = new AppUser
        {
            UserName = email,
            Email = email
        };

        var result = await userManager.CreateAsync(appUser, password);
        if (!result.Succeeded)
        {
            throw new Exception($"Failed to create test user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        await userManager.AddToRoleAsync(appUser, role.ToString());

        var province = await dbContext.Provinces.FirstOrDefaultAsync();
        if (province == null)
        {
            province = new Province { Name = "Test Province" };
            dbContext.Provinces.Add(province);
            await dbContext.SaveChangesAsync();
        }

        var domainUser = new User
        {
            Id = appUser.Id,
            Email = email,
            FirstName = "Test",
            LastName = "User",
            Role = role,
            ProvinceId = province.Id,
            IsActive = true
        };

        dbContext.UsersDomain.Add(domainUser);
        await dbContext.SaveChangesAsync();

        return appUser.Id;
    }

    /// <summary>
    /// Seeds a test province in the database
    /// </summary>
    public static async Task<int> SeedTestProvinceAsync(IServiceProvider serviceProvider, string name = "Test Province")
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var province = new Province { Name = name };
        dbContext.Provinces.Add(province);
        await dbContext.SaveChangesAsync();

        return province.Id;
    }
}

