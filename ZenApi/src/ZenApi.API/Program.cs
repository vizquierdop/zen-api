using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZenApi.Application;
using ZenApi.Infrastructure;
using ZenApi.Infrastructure.Persistence;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(AssemblyMarker).Assembly);
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(AssemblyMarker).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocument(config =>
{
    config.Title = "ZenApi v1";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwaggerUi(settings =>
{
    settings.DocumentPath = "/openapi/v1.json";
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

