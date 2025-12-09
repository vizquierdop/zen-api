using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Enums;

namespace ZenApi.Application.Dtos.Auth.Login
{
    public class LoginResultDto
    {
        public string AccessToken { get; set; } = default!;
        public DateTime AccessTokenExpiresAt { get; set; }

        public string RefreshToken { get; set; } = default!;
        public DateTime RefreshTokenExpiresAt { get; set; }

        public int UserId { get; set; }
        public string Email { get; set; } = default!;
        public UserRole Role { get; set; } = default!;
        public int? BusinessId { get; set; }
    }
}
