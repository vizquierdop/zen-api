using FluentAssertions;
using Moq;
using Xunit;
using ZenApi.Application.Models.Categories.Queries.GetAll;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Common.Models;
using ZenApi.Application.Common.Models.SearchModels;
using ZenApi.Application.Dtos.Categories;

namespace ZenApi.UnitTests.Handlers.Categories;

public class GetCategoriesQueryHandlerTests
{
    private readonly Mock<ICategoryQueryRepository> _repositoryMock;
    private readonly GetCategoriesQueryHandler _handler;

    public GetCategoriesQueryHandlerTests()
    {
        _repositoryMock = new Mock<ICategoryQueryRepository>();
        _handler = new GetCategoriesQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidSearchModel_ShouldReturnPaginatedList()
    {
        var searchModel = new CategorySearchModel
        {
            PaginationSkip = 1,
            PaginationLength = 10
        };

        var query = new GetCategoriesQuery(searchModel);

        var expectedResult = new PaginatedList<CategoryDto>(
            new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "Category 1" },
                new CategoryDto { Id = 2, Name = "Category 2" }
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
        _repositoryMock.Verify(r => r.GetAllAsync(searchModel, It.IsAny<CancellationToken>()), Times.Once);
    }
}

