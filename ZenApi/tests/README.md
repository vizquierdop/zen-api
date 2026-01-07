# Testing Guide

This directory contains the test projects for ZenApi.

## Structure

- **ZenApi.UnitTests**: Unit tests for handlers, services, validators, and other isolated components
- **ZenApi.IntegrationTests**: Integration tests for API endpoints and full request/response flows

## Running Tests

### Run all tests
```bash
dotnet test
```

### Run tests from a specific project
```bash
dotnet test tests/ZenApi.UnitTests
dotnet test tests/ZenApi.IntegrationTests
```

### Run tests with code coverage
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Run a specific test
```bash
dotnet test --filter "FullyQualifiedName~UpdateUserCommandHandlerTests"
```

### Run tests in watch mode (for development)
```bash
dotnet watch test
```

## Test Categories

### Unit Tests
- **Handlers**: Test MediatR command/query handlers with mocked dependencies
- **Services**: Test business logic services (JwtService, SecurityService, etc.)
- **Validators**: Test FluentValidation validators
- **Behaviours**: Test MediatR pipeline behaviours

### Integration Tests
- **Endpoints**: Test API controllers with in-memory database
- **Authentication**: Test JWT authentication flows
- **Full Flows**: Test complete user journeys

## Test Data

Integration tests use an in-memory database that is created fresh for each test class. Use the `TestDataSeeder` helper class to seed test data when needed.

## Best Practices

1. **Unit Tests**: Should be fast, isolated, and test a single unit of work
2. **Integration Tests**: Should test real interactions but use in-memory database
3. **Test Naming**: Use descriptive names that explain what is being tested
4. **Arrange-Act-Assert**: Follow the AAA pattern for test structure
5. **Mock External Dependencies**: Use Moq for unit tests, real implementations for integration tests

## Adding New Tests

### Unit Test Example
```csharp
[Fact]
public async Task Handle_WithValidInput_ShouldReturnExpectedResult()
{
    // Arrange
    var mockRepository = new Mock<IRepository>();
    var handler = new MyHandler(mockRepository.Object);
    
    // Act
    var result = await handler.Handle(command);
    
    // Assert
    result.Should().NotBeNull();
}
```

### Integration Test Example
```csharp
[Fact]
public async Task GetUser_WithValidId_ShouldReturnUser()
{
    // Arrange
    var userId = await TestDataSeeder.SeedTestUserAsync(_factory.Services, "test@example.com", "Password123!");
    
    // Act
    var response = await _client.GetAsync($"/api/users/{userId}");
    
    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
}
```

