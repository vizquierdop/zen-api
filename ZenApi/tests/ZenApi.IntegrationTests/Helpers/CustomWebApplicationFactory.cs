using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.IntegrationTests.Helpers;

/// <summary>
/// Custom WebApplicationFactory for integration tests
/// Configures in-memory database and test-specific services
/// </summary>
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var descriptorOptions = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptorOptions != null)
                services.Remove(descriptorOptions);

            var descriptorContext = services.SingleOrDefault(
                d => d.ServiceType == typeof(ApplicationDbContext));

            if (descriptorContext != null)
                services.Remove(descriptorContext);

            var descriptorConnection = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbConnection));

            if (descriptorConnection != null)
                services.Remove(descriptorConnection);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb_" + Guid.NewGuid().ToString());
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();
            
            db.Database.EnsureCreated();
        });
    }
}

