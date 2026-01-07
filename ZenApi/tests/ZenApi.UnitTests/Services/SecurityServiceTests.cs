// using FluentAssertions;
// using Moq;
// using Xunit;
// using ZenApi.Application.Common.Interfaces;
// using ZenApi.Domain.Enums;
// using ZenApi.Infrastructure.Identity;
// using ZenApi.Infrastructure.Services;
// using Microsoft.AspNetCore.Identity;

// namespace ZenApi.UnitTests.Services;

// public class SecurityServiceTests
// {
//     private readonly Mock<UserManager<AppUser>> _userManagerMock;
//     private readonly Mock<SignInManager<AppUser>> _signInManagerMock;
//     private readonly SecurityService _service;

//     public SecurityServiceTests()
//     {
//         var store = new Mock<IUserStore<AppUser>>();
//         _userManagerMock = new Mock<UserManager<AppUser>>(
//             store.Object, 
//             null!, 
//             null!, 
//             null!, 
//             null!, 
//             null!, 
//             null!, 
//             null!, 
//             null!);

//         var contextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
//         var claimsFactory = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
//         var options = new Mock<Microsoft.Extensions.Options.IOptions<IdentityOptions>>();
//         var logger = new Mock<Microsoft.Extensions.Logging.ILogger<SignInManager<AppUser>>>();
//         var schemes = new Mock<Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider>();
//         var confirmation = new Mock<IUserConfirmation<AppUser>>();

//         _signInManagerMock = new Mock<SignInManager<AppUser>>(
//             _userManagerMock.Object,
//             contextAccessor.Object,
//             claimsFactory.Object,
//             options.Object,
//             logger.Object,
//             schemes.Object,
//             confirmation.Object);

//         _service = new SecurityService(_userManagerMock.Object, _signInManagerMock.Object);
//     }

//     // [Fact]
//     // public void HashPassword_ShouldReturnHashedPassword()
//     // {
//     //     var password = "Pa$$w0rd";

//     //     var hashedPassword = _service.HashPassword(password);

//     //     hashedPassword.Should().NotBeNullOrEmpty();
//     //     hashedPassword.Should().NotBe(password);
//     //     hashedPassword.Length.Should().BeGreaterThan(password.Length);
//     // }

//     [Fact]
//     public void HashPassword_WithSamePassword_ShouldReturnDifferentHashes()
//     {
//         var password = "Pa$$w0rd";

//         var hash1 = _service.HashPassword(password);
//         var hash2 = _service.HashPassword(password);

//         hash1.Should().NotBe(hash2);
//     }

//     [Fact]
//     public void VerifyPassword_WithCorrectPassword_ShouldReturnTrue()
//     {
//         var password = "Pa$$w0rd";
//         var hashedPassword = _service.HashPassword(password);

//         var result = _service.VerifyPassword(password, hashedPassword);

//         result.Should().BeTrue();
//     }

//     [Fact]
//     public void VerifyPassword_WithIncorrectPassword_ShouldReturnFalse()
//     {
//         var password = "Pa$$w0rd";
//         var wrongPassword = "Passw0rd!";
//         var hashedPassword = _service.HashPassword(password);

//         var result = _service.VerifyPassword(wrongPassword, hashedPassword);

//         result.Should().BeFalse();
//     }

//     // [Fact]
//     // public async Task CheckPasswordAsync_WithValidCredentials_ShouldReturnTrue()
//     // {
//     //     var email = "test@gmail.com";
//     //     var password = "Pa$$w0rd";
//     //     var appUser = new AppUser { Id = 1, Email = email, UserName = email };

//     //     _userManagerMock
//     //         .Setup(u => u.FindByEmailAsync(email))
//     //         .ReturnsAsync(appUser);

//     //     _signInManagerMock
//     //         .Setup(s => s.CheckPasswordSignInAsync(appUser, password, false))
//     //         .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

//     //     var result = await _service.CheckPasswordAsync(email, password);

//     //     result.Should().BeTrue();
//     // }

//     // [Fact]
//     // public async Task CheckPasswordAsync_WithInvalidEmail_ShouldReturnFalse()
//     // {
//     //     var email = "nonexistent@gmail.com";
//     //     var password = "Pa$$w0rd";

//     //     _userManagerMock
//     //         .Setup(u => u.FindByEmailAsync(email))
//     //         .ReturnsAsync((AppUser?)null);

//     //     var result = await _service.CheckPasswordAsync(email, password);

//     //     result.Should().BeFalse();
//     // }

//     // [Fact]
//     // public async Task CheckPasswordAsync_WithInvalidPassword_ShouldReturnFalse()
//     // {
//     //     var email = "test@gmail.com";
//     //     var password = "Passw0rd!";
//     //     var appUser = new AppUser { Id = 1, Email = email, UserName = email };

//     //     _userManagerMock
//     //         .Setup(u => u.FindByEmailAsync(email))
//     //         .ReturnsAsync(appUser);

//     //     _signInManagerMock
//     //         .Setup(s => s.CheckPasswordSignInAsync(appUser, password, false))
//     //         .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

//     //     var result = await _service.CheckPasswordAsync(email, password);

//     //     result.Should().BeFalse();
//     // }
// }

