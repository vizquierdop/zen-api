using FluentAssertions;
using Moq;
using Xunit;
using ZenApi.Application.Models.Reservations.Commands.Create;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Domain.Entities;
using ZenApi.Domain.Enums;
using AutoMapper;

namespace ZenApi.UnitTests.Handlers.Reservations;

public class CreateReservationCommandHandlerTests
{
    private readonly Mock<IReservationCommandRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateReservationCommandHandler _handler;

    public CreateReservationCommandHandlerTests()
    {
        _repositoryMock = new Mock<IReservationCommandRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateReservationCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateReservationWithPendingStatus()
    {
        var command = new CreateReservationCommand
        {
            Date = DateTime.Today.AddDays(7),
            StartTime = "10:00",
            EndTime = "11:00",
            CustomerName = "John Doe",
            CustomerEmail = "john@example.com",
            ServiceId = 1,
            UserId = 1
        };

        var mappedReservation = new Reservation
        {
            Date = command.Date,
            StartTime = command.StartTime,
            EndTime = command.EndTime,
            Status = ReservationStatus.Pending,
            CustomerName = command.CustomerName,
            CustomerEmail = command.CustomerEmail,
            ServiceId = command.ServiceId,
            UserId = command.UserId,
            Service = new Domain.Entities.OfferedService
            {
                Id = command.ServiceId,
                Name = "Test Service",
                IsActive = true,
                BusinessId = 1,
                Business = new Domain.Entities.Business
                {
                    Id = 1,
                    Name = "Test Business",
                    Address = "Test Address",
                    Phone = "123456789",
                    ProvinceId = 1
                }
            }
        };

        var expectedId = 123;

        _mapperMock
            .Setup(m => m.Map<Reservation>(command))
            .Returns(mappedReservation);

        _repositoryMock
            .Setup(r => r.CreateAsync(It.Is<Reservation>(res => res.Status == ReservationStatus.Pending), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedId);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(expectedId);
        mappedReservation.Status.Should().Be(ReservationStatus.Pending);
        _mapperMock.Verify(m => m.Map<Reservation>(command), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(mappedReservation, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNullUserId_ShouldStillCreateReservation()
    {
        var command = new CreateReservationCommand
        {
            Date = DateTime.Today.AddDays(7),
            StartTime = "10:00",
            EndTime = "11:00",
            ServiceId = 1,
            UserId = null
        };

        var mappedReservation = new Reservation
        {
            Date = command.Date,
            StartTime = command.StartTime,
            EndTime = command.EndTime,
            Status = ReservationStatus.Pending,
            ServiceId = command.ServiceId,
            UserId = null,
            Service = new Domain.Entities.OfferedService
            {
                Id = command.ServiceId,
                Name = "Test Service",
                IsActive = true,
                BusinessId = 1,
                Business = new Domain.Entities.Business
                {
                    Id = 1,
                    Name = "Test Business",
                    Address = "Test Address",
                    Phone = "123456789",
                    ProvinceId = 1
                }
            }
        };

        _mapperMock
            .Setup(m => m.Map<Reservation>(command))
            .Returns(mappedReservation);

        _repositoryMock
            .Setup(r => r.CreateAsync(It.IsAny<Reservation>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(1);
        mappedReservation.Status.Should().Be(ReservationStatus.Pending);
    }
}

