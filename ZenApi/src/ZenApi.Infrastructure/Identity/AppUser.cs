using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Entities;

namespace ZenApi.Infrastructure.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public User DomainUser { get; set; } = null!;
    }
}
