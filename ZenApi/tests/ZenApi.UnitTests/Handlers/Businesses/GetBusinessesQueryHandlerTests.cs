using FluentAssertions;
using Moq;
using Xunit;
using ZenApi.Application.Models.Businesses.Queries.GetAll;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Businesses;

namespace ZenApi.UnitTests.Handlers.Businesses;

public class GetBusinessesQueryHandlerTests
{
    private readonly Mock<IBusinessQueryRepository> _repositoryMock;
    private readonly GetBusinessesQueryHandler _handler;

    public GetBusinessesQueryHandlerTests()
    {
        _repositoryMock = new Mock<IBusinessQueryRepository>();
        _handler = new GetBusinessesQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidSearchModel_ShouldReturnPaginatedList()
    {
        var searchModel = new BusinessSearchModel
        {
            PaginationSkip = 1,
            PaginationLength = 10
        };

        var query = new GetBusinessesQuery(searchModel);

        var expectedResult = new PaginatedList<BusinessDto>(
            new List<BusinessDto>
            {
                new BusinessDto { Id = 1, Name = "Business 1", Address = "Address 1", Phone = "123456789" },
                new BusinessDto { Id = 2, Name = "Business 2", Address = "Address 2", Phone = "987654321" }
            },
            2,
            1,
            10
        );

        _repositoryMock
            .Setup(r => r.GetAllAsync(searchModel, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(2);
        result.PageNumber.Should().Be(1);
        _repositoryMock.Verify(r => r.GetAllAsync(searchModel, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithEmptyResult_ShouldReturnEmptyPaginatedList()
    {
        var searchModel = new BusinessSearchModel
        {
            PaginationSkip = 1,
            PaginationLength = 10
        };

        var query = new GetBusinessesQuery(searchModel);

        var expectedResult = new PaginatedList<BusinessDto>(
            new List<BusinessDto>(),
            0,
            1,
            10
        );

        _repositoryMock
            .Setup(r => r.GetAllAsync(searchModel, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
    }
}

