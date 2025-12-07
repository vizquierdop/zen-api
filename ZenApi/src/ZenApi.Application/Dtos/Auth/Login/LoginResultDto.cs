using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Role { get; set; } = default!;
    }
}
