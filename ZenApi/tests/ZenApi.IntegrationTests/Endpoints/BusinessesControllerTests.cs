using System.Net;
using FluentAssertions;
using Xunit;
using ZenApi.API;
using ZenApi.IntegrationTests.Helpers;

namespace ZenApi.IntegrationTests.Endpoints;

public class BusinessesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public BusinessesControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var response = await _client.GetAsync("/api/businesses");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetById_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var businessId = 1;

        var response = await _client.GetAsync($"/api/businesses/{businessId}");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Update_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var updateCommand = new
        {
            Id = 1,
            Name = "Updated Business"
        };

        var response = await _client.PutAsJsonAsync("/api/businesses/1", updateCommand);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}

