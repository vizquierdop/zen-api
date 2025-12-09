using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Dtos.Auth.Login;

namespace ZenApi.Application.Models.Auth.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<LoginResultDto>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultDto>
    {
        private readonly ISecurityService _securityService;
        private readonly IJwtService _jwtService;
        private readonly IUserCommandRepository _users;
        private readonly IRefreshTokenRepository _refreshTokens;

        public LoginCommandHandler(ISecurityService securityService, IJwtService jwtService, IUserCommandRepository users,
        IRefreshTokenRepository refreshTokens)
        {
            _securityService = securityService;
            _jwtService = jwtService;
            _users = users;
            _refreshTokens = refreshTokens;
        }

        public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var isValid = await _securityService.CheckPasswordAsync(request.Email, request.Password);

            if (!isValid)
                throw new UnauthorizedAccessException("Invalid credentials");

            var appUserId = await _securityService.GetAppUserIdByEmailAsync(request.Email)
            ?? throw new UnauthorizedAccessException("User not found");

            var domainUser = await _users.GetByIdAsync(appUserId, cancellationToken)
                ?? throw new UnauthorizedAccessException("Domain user not found");

            var accessToken = _jwtService.GenerateAccessToken(domainUser);
            var refreshToken = _jwtService.GenerateRefreshToken(domainUser.Id);

            await _refreshTokens.SaveAsync(refreshToken, cancellationToken);

            return new LoginResultDto
            {
                AccessToken = accessToken,
                AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(60),

                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresAt = refreshToken.Expires,

                UserId = domainUser.Id,
                Email = domainUser.Email,
                Role = domainUser.Role,
                BusinessId = domainUser.BusinessId,
            };

            //var userId = await _securityService.GetUserIdByEmailAsync(request.Email);

            //var (token, expiresAt) = _jwtService.GenerateToken(userId, request.Email);

            //return new LoginResultDto
            //{
            //    Token = token,
            //    ExpiresAt = expiresAt
            //};
        }
    }
}
