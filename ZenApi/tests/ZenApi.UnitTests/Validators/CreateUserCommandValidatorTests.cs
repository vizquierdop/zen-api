using FluentAssertions;
using Xunit;
using ZenApi.Application.Models.Users.Commands.Create;
using ZenApi.Domain.Enums;

namespace ZenApi.UnitTests.Validators;

public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _validator;

    public CreateUserCommandValidatorTests()
    {
        _validator = new CreateUserCommandValidator();
    }

    [Fact]
    public void Validate_WithValidCommand_ShouldBeValid()
    {
        var command = new CreateUserCommand
        {
            Email = "test@exagmailmple.com",
            Password = "Pa$$w0rd",
            FirstName = "Test",
            LastName = "Customer",
            Role = UserRole.Customer,
            ProvinceId = 1
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithInvalidEmail_ShouldBeInvalid()
    {
        var command = new CreateUserCommand
        {
            Email = "invalid-email.com",
            Password = "Pa$$w0rd",
            FirstName = "Test",
            Role = UserRole.Customer,
            ProvinceId = 1
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email");
    }

    [Fact]
    public void Validate_WithEmptyEmail_ShouldBeInvalid()
    {
        var command = new CreateUserCommand
        {
            Email = string.Empty,
            Password = "Pa$$w0rd",
            FirstName = "Test",
            Role = UserRole.Customer,
            ProvinceId = 1
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email");
    }

    [Fact]
    public void Validate_WithShortPassword_ShouldBeInvalid()
    {
        var command = new CreateUserCommand
        {
            Email = "test@gmail.com",
            Password = "12345",
            FirstName = "Test",
            Role = UserRole.Customer,
            ProvinceId = 1
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Password");
    }

    [Fact]
    public void Validate_WithEmptyPassword_ShouldBeInvalid()
    {
        var command = new CreateUserCommand
        {
            Email = "test@gmail.com",
            Password = string.Empty,
            FirstName = "Test",
            Role = UserRole.Customer,
            ProvinceId = 1
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Password");
    }

    [Fact]
    public void Validate_WithBusinessRoleAndNullBusiness_ShouldBeInvalid()
    {
        var command = new CreateUserCommand
        {
            Email = "business@gmail.com",
            Password = "Pa$$w0rd",
            FirstName = "Business",
            Role = UserRole.Business,
            ProvinceId = 1,
            Business = null
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Business");
    }

    [Fact]
    public void Validate_WithBusinessRoleAndValidBusiness_ShouldBeValid()
    {
        var command = new CreateUserCommand
        {
            Email = "business@gmail.com",
            Password = "Pa$$w0rd",
            FirstName = "Business",
            Role = UserRole.Business,
            ProvinceId = 1,
            Business = new Application.Dtos.Businesses.CreateBusinessDto
            {
                Name = "Test Business",
                Address = "123 Test St",
                Keyword1 = "keyword1",
                Keyword2 = "keyword2",
                Keyword3 = "keyword3",
                ProvinceId = 1
            }
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithClientRoleAndNullBusiness_ShouldBeValid()
    {
        var command = new CreateUserCommand
        {
            Email = "customer@gmail.com",
            Password = "Pa$$w0rd",
            FirstName = "Customer",
            Role = UserRole.Customer,
            ProvinceId = 1,
            Business = null
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }
}

