using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Mappings;
using ZenApi.Domain.Entities;
using ZenApi.Domain.Enums;

namespace ZenApi.Application.Dtos.Users
{
    public class UserShortDto : IMapFrom<User>
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
    }
}