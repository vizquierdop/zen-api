using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Dtos.Auth.RefreshToken
{
    public class RefreshTokenRequestDto
    {
        public string Token { get; set; } = default!;
    }
}
