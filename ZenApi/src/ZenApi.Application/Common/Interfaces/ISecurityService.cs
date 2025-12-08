using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Entities;
using ZenApi.Domain.Enums;

namespace ZenApi.Application.Common.Interfaces
{
    public interface ISecurityService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        Task<int> CreateUserAsync(string email, string password, UserRole role);
        Task<bool> CheckPasswordAsync(string email, string password);
        Task AddRoleAsync(int appUserId, UserRole role);
        Task<int> GetUserIdByEmailAsync(string email);
        Task<int?> GetAppUserIdByEmailAsync(string email);
    }
}
