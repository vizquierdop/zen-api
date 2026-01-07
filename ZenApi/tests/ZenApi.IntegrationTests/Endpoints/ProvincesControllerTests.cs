using System.Net;
using FluentAssertions;
using Xunit;
using ZenApi.API;
using ZenApi.IntegrationTests.Helpers;

namespace ZenApi.IntegrationTests.Endpoints;

public class ProvincesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public ProvincesControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/provinces");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAll_WithQueryParameters_ShouldReturnOk()
    {
        var queryString = "?PageNumber=1&PageSize=10";

        var response = await _client.GetAsync($"/api/provinces{queryString}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}

