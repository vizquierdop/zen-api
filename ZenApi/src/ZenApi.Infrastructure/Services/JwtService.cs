using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using JwtNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using ZenApi.Application.Common.Interfaces;

namespace ZenApi.Infrastructure.Services
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int AccessTokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationDays { get; set; }
    }

    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;

        public JwtService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public string GenerateAccessToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Cannot generate token for a null user.");

            var tokenHandler = new JwtSecurityTokenHandler();
            if (string.IsNullOrWhiteSpace(_settings.Secret))
                throw new InvalidOperationException("JwtSettings:Secret no está configurado.");

            var key = Encoding.ASCII.GetBytes(_settings.Secret);

            var claims = new List<Claim>
        {
            new Claim(JwtNames.Sub, user.Id.ToString()),
            new Claim(JwtNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(int userId)
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            var token = Convert.ToBase64String(randomBytes);

            return new RefreshToken
            {
                Token = token,
                Expires = DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationDays),
                Created = DateTime.UtcNow,
                UserId = userId
            };
        }
    }
}
