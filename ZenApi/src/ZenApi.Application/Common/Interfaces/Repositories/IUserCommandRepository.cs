using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IUserCommandRepository
    {
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(User entity, CancellationToken cancellationToken);
        Task<int> CreateAsync(User entity, CancellationToken cancellationToken);
        Task AttachBusinessAsync(int userId, int businessId, CancellationToken cancellationToken);
        Task<int> CreateUserWithBusinessAsync(User user, Business business, CancellationToken cancellationToken);
    }
}
