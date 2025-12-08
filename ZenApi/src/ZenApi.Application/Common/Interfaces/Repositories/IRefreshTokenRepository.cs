using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task SaveAsync(RefreshToken token, CancellationToken cancellationToken);
        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken);
        Task InvalidateAsync(string token, CancellationToken cancellationToken);
    }
}
