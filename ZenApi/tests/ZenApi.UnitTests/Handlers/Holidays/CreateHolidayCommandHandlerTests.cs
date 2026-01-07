using FluentAssertions;
using Moq;
using Xunit;
using ZenApi.Application.Models.Holidays.Commands.Create;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Domain.Entities;
using AutoMapper;

namespace ZenApi.UnitTests.Handlers.Holidays;

public class CreateHolidayCommandHandlerTests
{
    private readonly Mock<IHolidayCommandRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateHolidayCommand.CreateHolidayCommandHandler _handler;

    public CreateHolidayCommandHandlerTests()
    {
        _repositoryMock = new Mock<IHolidayCommandRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateHolidayCommand.CreateHolidayCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateHoliday()
    {
        var startDate = DateTime.Today.AddDays(10);
        var endDate = DateTime.Today.AddDays(15);
        var command = new CreateHolidayCommand
        {
            StartDate = startDate,
            EndDate = endDate,
            BusinessId = 1
        };

        var mappedHoliday = new Holiday
        {
            StartDate = startDate,
            EndDate = endDate,
            BusinessId = 1
        };

        var expectedId = 456;

        _mapperMock
            .Setup(m => m.Map<Holiday>(command))
            .Returns(mappedHoliday);

        _repositoryMock
            .Setup(r => r.CreateAsync(mappedHoliday, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedId);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(expectedId);
        _mapperMock.Verify(m => m.Map<Holiday>(command), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(mappedHoliday, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithEndDateBeforeStartDate_ShouldStillCreateHoliday()
    {
        var startDate = DateTime.Today.AddDays(15);
        var endDate = DateTime.Today.AddDays(10);
        var command = new CreateHolidayCommand
        {
            StartDate = startDate,
            EndDate = endDate,
            BusinessId = 1
        };

        var mappedHoliday = new Holiday
        {
            StartDate = startDate,
            EndDate = endDate,
            BusinessId = 1
        };

        _mapperMock
            .Setup(m => m.Map<Holiday>(command))
            .Returns(mappedHoliday);

        _repositoryMock
            .Setup(r => r.CreateAsync(It.IsAny<Holiday>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(1);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Holiday>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}

