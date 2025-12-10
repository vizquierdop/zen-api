using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;

namespace ZenApi.Application.Models.Auth.RevokeToken
{
    public record RevokeTokenCommand(string Token) : IRequest;

    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RevokeTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(request.Token, cancellationToken);
            if (token != null && token.IsActive)
            {
                token.Revoked = DateTime.UtcNow;
                token.ReplacedByToken = null;

                await _refreshTokenRepository.UpdateAsync(token, cancellationToken);
            }
        }
    }
}
