using FluentAssertions;
using Moq;
using Xunit;
using ZenApi.Application.Models.Users.Commands.Update;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Domain.Entities;
using AutoMapper;

namespace ZenApi.UnitTests.Handlers.Users;

public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUserCommandRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        _repositoryMock = new Mock<IUserCommandRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateUserCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WhenUserExists_ShouldUpdateUser()
    {
        var userId = 1;
        var command = new UpdateUserCommand
        {
            Id = userId,
            FirstName = "UpdatedName",
            LastName = "UpdatedLastName"
        };

        var existingUser = new User
        {
            Id = userId,
            Email = "test@gmail.com",
            FirstName = "OriginalName",
            LastName = "OriginalLastName",
            ProvinceId = 1
        };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        await _handler.Handle(command, CancellationToken.None);

        _repositoryMock.Verify(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(m => m.Map(command, existingUser), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(existingUser, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldThrowException()
    {
        var command = new UpdateUserCommand { Id = 999 };
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        await Assert.ThrowsAsync<Exception>(() => 
            _handler.Handle(command, CancellationToken.None));
        
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}

