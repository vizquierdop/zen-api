using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Dtos.Users;

namespace ZenApi.Application.Common.Interfaces.Repositories
{
    public interface IUserQueryRepository
    {
        Task<UserDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
