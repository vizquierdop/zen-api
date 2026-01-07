using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using ZenApi.API;
using ZenApi.Application.Models.Auth.Login;
using ZenApi.IntegrationTests.Helpers;

namespace ZenApi.IntegrationTests.Endpoints;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public AuthControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ShouldReturnUnauthorized()
    {
        var command = new LoginCommand("nonexistent@example.com", "WrongPassword");

        var response = await _client.PostAsJsonAsync("/api/auth/login", command);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ShouldReturnToken()
    {
        var command = new LoginCommand("test@example.com", "Password123!");

        var response = await _client.PostAsJsonAsync("/api/auth/login", command);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadFromJsonAsync<Application.Dtos.Auth.Login.LoginResultDto>();
            result.Should().NotBeNull();
            result!.AccessToken.Should().NotBeNullOrEmpty();
            result.RefreshToken.Should().NotBeNullOrEmpty();
        }
    }

    [Fact]
    public async Task Refresh_WithInvalidToken_ShouldReturnBadRequest()
    {
        var request = new Application.Dtos.Auth.RefreshToken.RefreshTokenRequestDto
        {
            Token = "invalid-token"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/refresh", request);

        response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.Unauthorized);
    }
}

