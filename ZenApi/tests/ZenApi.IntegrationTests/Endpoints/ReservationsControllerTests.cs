using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using ZenApi.API;
using ZenApi.Application.Models.Reservations.Commands.Create;
using ZenApi.IntegrationTests.Helpers;

namespace ZenApi.IntegrationTests.Endpoints;

public class ReservationsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public ReservationsControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var response = await _client.GetAsync("/api/reservations");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Create_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var command = new CreateReservationCommand
        {
            Date = DateTime.Today.AddDays(7),
            StartTime = "10:00",
            EndTime = "11:00",
            ServiceId = 1
        };

        var response = await _client.PostAsJsonAsync("/api/reservations", command);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Update_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var updateCommand = new
        {
            Id = 1,
            Date = DateTime.Today.AddDays(7)
        };

        var response = await _client.PutAsJsonAsync("/api/reservations/1", updateCommand);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Delete_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var reservationId = 1;

        var response = await _client.DeleteAsync($"/api/reservations/{reservationId}");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}

