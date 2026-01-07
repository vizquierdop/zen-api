using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using ZenApi.API;
using ZenApi.IntegrationTests.Helpers;
using ZenApi.Application.Models.Users.Commands.Create;
using ZenApi.Domain.Enums;

namespace ZenApi.IntegrationTests.Endpoints;

public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public UsersControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task CreateUser_WithValidData_ShouldReturnCreatedUserId()
    {
        var userData = new CreateUserCommand
        {
            Email = $"test_{Guid.NewGuid()}@gmail.com",
            Password = "Pa$$w0rd",
            FirstName = "Test",
            LastName = "Customer",
            Role = UserRole.Customer,
            ProvinceId = 1
        };

        var response = await _client.PostAsJsonAsync("/api/users", userData);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var userId = await response.Content.ReadFromJsonAsync<int>();
        userId.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task CreateUser_WithInvalidEmail_ShouldReturnBadRequest()
    {
        var userData = new CreateUserCommand
        {
            Email = "invalid-email.com",
            Password = "Pa$$w0rd",
            FirstName = "Test",
            Role = UserRole.Customer,
            ProvinceId = 1
        };

        var response = await _client.PostAsJsonAsync("/api/users", userData);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateUser_WithShortPassword_ShouldReturnBadRequest()
    {
        var userData = new CreateUserCommand
        {
            Email = $"test_{Guid.NewGuid()}@gmail.com",
            Password = "12345",
            FirstName = "Test",
            Role = UserRole.Customer,
            ProvinceId = 1
        };

        var response = await _client.PostAsJsonAsync("/api/users", userData);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetUser_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var userId = 1;

        var response = await _client.GetAsync($"/api/users/{userId}");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateUser_WithoutAuthentication_ShouldReturnUnauthorized()
    {
        var updateCommand = new Application.Models.Users.Commands.Update.UpdateUserCommand
        {
            Id = 1,
            FirstName = "Updated"
        };

        var response = await _client.PutAsJsonAsync("/api/users/1", updateCommand);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateUser_WithMismatchedIds_ShouldReturnBadRequest()
    {
        var updateCommand = new Application.Models.Users.Commands.Update.UpdateUserCommand
        {
            Id = 1,
            FirstName = "Updated"
        };

        var response = await _client.PutAsJsonAsync("/api/users/2", updateCommand);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}

