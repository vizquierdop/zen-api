using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;
using ZenApi.Infrastructure.Services;
using ZenApi.Domain.Entities;
using ZenApi.Domain.Enums;

namespace ZenApi.UnitTests.Services;

public class JwtServiceTests
{
    private readonly JwtService _jwtService;
    private readonly JwtSettings _settings;

    public JwtServiceTests()
    {
        _settings = new JwtSettings
        {
            Secret = "ThisIsAVeryLongSecretKeyForTestingPurposes123456789",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            AccessTokenExpirationMinutes = 60,
            RefreshTokenExpirationDays = 7
        };

        var options = Options.Create(_settings);
        _jwtService = new JwtService(options);
    }

    [Fact]
    public void GenerateAccessToken_WithValidUser_ShouldReturnToken()
    {
        var user = new User
        {
            Id = 1,
            Email = "test@example.com",
            FirstName = "Test",
            Role = UserRole.Customer,
            ProvinceId = 1
        };

        var token = _jwtService.GenerateAccessToken(user);

        token.Should().NotBeNullOrEmpty();
        token.Split('.').Should().HaveCount(3); // JWT has 3 parts separated by dots
    }

    [Fact]
    public void GenerateAccessToken_WithNullUser_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => 
            _jwtService.GenerateAccessToken(null!));
    }

    [Fact]
    public void GenerateAccessToken_WithDifferentRoles_ShouldGenerateDifferentTokens()
    {
        var clientUser = new User
        {
            Id = 1,
            Email = "customer@gmail.com",
            FirstName = "Customer",
            Role = UserRole.Customer,
            ProvinceId = 1
        };

        var businessUser = new User
        {
            Id = 2,
            Email = "business@gmail.com",
            FirstName = "Business",
            Role = UserRole.Business,
            ProvinceId = 1
        };

        var clientToken = _jwtService.GenerateAccessToken(clientUser);
        var businessToken = _jwtService.GenerateAccessToken(businessUser);

        clientToken.Should().NotBe(businessToken);
    }

    [Fact]
    public void GenerateRefreshToken_ShouldReturnValidRefreshToken()
    {
        var userId = 1;

        var refreshToken = _jwtService.GenerateRefreshToken(userId);

        refreshToken.Should().NotBeNull();
        refreshToken.Token.Should().NotBeNullOrEmpty();
        refreshToken.UserId.Should().Be(userId);
        refreshToken.Expires.Should().BeAfter(DateTime.UtcNow);
        refreshToken.Created.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void GenerateRefreshToken_WithDifferentUsers_ShouldGenerateDifferentTokens()
    {
        var userId1 = 1;
        var userId2 = 2;

        var token1 = _jwtService.GenerateRefreshToken(userId1);
        var token2 = _jwtService.GenerateRefreshToken(userId2);

        token1.Token.Should().NotBe(token2.Token);
    }
}

