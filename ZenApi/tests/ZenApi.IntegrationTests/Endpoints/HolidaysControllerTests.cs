using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using ZenApi.API;
using ZenApi.Application.Models.Holidays.Commands.Create;
using ZenApi.IntegrationTests.Helpers;

namespace ZenApi.IntegrationTests.Endpoints;

public class HolidaysControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public HolidaysControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var response = await _client.GetAsync("/api/holidays");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Create_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var command = new CreateHolidayCommand
        {
            StartDate = DateTime.Today.AddDays(10),
            EndDate = DateTime.Today.AddDays(15),
            BusinessId = 1
        };

        var response = await _client.PostAsJsonAsync("/api/holidays", command);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Delete_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var holidayId = 1;

        var response = await _client.DeleteAsync($"/api/holidays/{holidayId}");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}

