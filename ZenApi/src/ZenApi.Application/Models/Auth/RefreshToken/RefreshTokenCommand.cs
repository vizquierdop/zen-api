using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Application.Dtos.Auth.RefreshToken;

namespace ZenApi.Application.Models.Auth.RefreshToken
{
    public record RefreshTokenCommand(string Token) : IRequest<RefreshTokenResponseDto>;

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDto>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;

        public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IJwtService jwtService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
        }

        public async Task<RefreshTokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var existingToken = await _refreshTokenRepository.GetByTokenAsync(request.Token, cancellationToken);

            if (existingToken == null || !existingToken.IsActive)
                throw new UnauthorizedAccessException("Invalid refresh token");

            var newAccessToken = _jwtService.GenerateAccessToken(existingToken.User);
            var newRefreshToken = _jwtService.GenerateRefreshToken(existingToken.User.Id);

            await _refreshTokenRepository.SaveAsync(newRefreshToken, cancellationToken);

            existingToken.Revoked = DateTime.UtcNow;
            existingToken.ReplacedByToken = newRefreshToken.Token;

            await _refreshTokenRepository.SaveAsync(existingToken, cancellationToken);

            return new RefreshTokenResponseDto
            {
                AccessToken = newAccessToken,
                AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(60),
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiresAt = newRefreshToken.Expires
            };
        }
    }
}
