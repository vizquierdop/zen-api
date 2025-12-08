using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces.Repositories;
using ZenApi.Domain.Entities;
using ZenApi.Infrastructure.Persistence;

namespace ZenApi.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(RefreshToken token, CancellationToken cancellationToken)
        {
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == token, cancellationToken);
        }

        public async Task InvalidateAsync(string token, CancellationToken cancellationToken)
        {
            var refresh = await GetByTokenAsync(token, cancellationToken);
            if (refresh == null) return;

            refresh.Revoked = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
