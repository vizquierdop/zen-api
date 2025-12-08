using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;
using ZenApi.Domain.Enums;
using ZenApi.Infrastructure.Identity;

namespace ZenApi.Infrastructure.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SecurityService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task AddRoleAsync(int appUserId, UserRole role)
        {
            var user = await _userManager.FindByIdAsync(appUserId.ToString());
            if (user == null) throw new Exception("User not found");
            await _userManager.AddToRoleAsync(user, role.ToString());
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded;
        }

        public async Task<int> CreateUserAsync(string email, string password, UserRole role)
        {
            var appUser = new AppUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(appUser, role.ToString());
            return appUser.Id;
        }

        public async Task<int> GetUserIdByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new Exception("User not found");
            return user.Id;
        }

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                100000,
                HashAlgorithmName.SHA256,
                32
            );

            return Convert.ToBase64String(salt.Concat(hash).ToArray());
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var decoded = Convert.FromBase64String(hashedPassword);

            byte[] salt = decoded[..16];
            byte[] storedHash = decoded[16..];

            var hashToCheck = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                100000,
                HashAlgorithmName.SHA256,
                32
            );

            return CryptographicOperations.FixedTimeEquals(storedHash, hashToCheck);
        }

        public async Task<int?> GetAppUserIdByEmailAsync(string email)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
            return appUser?.Id;
        }
    }
}
