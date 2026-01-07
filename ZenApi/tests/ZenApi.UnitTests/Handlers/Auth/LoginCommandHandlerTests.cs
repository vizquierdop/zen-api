using FluentAssertions;
using Moq;
using Xunit;
using ZenApi.Application.Models.Auth.Login;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Domain.Entities;
using ZenApi.Domain.Enums;

namespace ZenApi.UnitTests.Handlers.Auth;

public class LoginCommandHandlerTests
{
    private readonly Mock<ISecurityService> _securityServiceMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly Mock<IUserCommandRepository> _usersMock;
    private readonly Mock<IRefreshTokenRepository> _refreshTokensMock;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        _securityServiceMock = new Mock<ISecurityService>();
        _jwtServiceMock = new Mock<IJwtService>();
        _usersMock = new Mock<IUserCommandRepository>();
        _refreshTokensMock = new Mock<IRefreshTokenRepository>();
        
        _handler = new LoginCommandHandler(
            _securityServiceMock.Object,
            _jwtServiceMock.Object,
            _usersMock.Object,
            _refreshTokensMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCredentials_ShouldReturnLoginResult()
    {
        var email = "test@example.com";
        var password = "Password123!";
        var command = new LoginCommand(email, password);
        var appUserId = 1;

        var domainUser = new User
        {
            Id = appUserId,
            Email = email,
            FirstName = "Test",
            Role = UserRole.Customer,
            ProvinceId = 1
        };

        var accessToken = "access-token";
        var refreshToken = new RefreshToken
        {
            Token = "refresh-token",
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = appUserId
        };

        _securityServiceMock
            .Setup(s => s.CheckPasswordAsync(email, password))
            .ReturnsAsync(true);

        _securityServiceMock
            .Setup(s => s.GetAppUserIdByEmailAsync(email))
            .ReturnsAsync(appUserId);

        _usersMock
            .Setup(u => u.GetByIdAsync(appUserId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(domainUser);

        _jwtServiceMock
            .Setup(j => j.GenerateAccessToken(domainUser))
            .Returns(accessToken);

        _jwtServiceMock
            .Setup(j => j.GenerateRefreshToken(appUserId))
            .Returns(refreshToken);

        var result = await _handler.Handle(command, CancellationToken.None);
        
        result.Should().NotBeNull();
        result.AccessToken.Should().Be(accessToken);
        result.RefreshToken.Should().Be(refreshToken.Token);
        result.UserId.Should().Be(appUserId);
        result.Email.Should().Be(email);
        
        _refreshTokensMock.Verify(r => r.SaveAsync(refreshToken, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidCredentials_ShouldThrowUnauthorizedAccessException()
    {
        var command = new LoginCommand("test@example.com", "WrongPassword");

        _securityServiceMock
            .Setup(s => s.CheckPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => 
            _handler.Handle(command, CancellationToken.None));
        
        _jwtServiceMock.Verify(j => j.GenerateAccessToken(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenUserNotFound_ShouldThrowUnauthorizedAccessException()
    {
        var command = new LoginCommand("test@example.com", "Password123!");

        _securityServiceMock
            .Setup(s => s.CheckPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        _securityServiceMock
            .Setup(s => s.GetAppUserIdByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((int?)null);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => 
            _handler.Handle(command, CancellationToken.None));
    }
}

