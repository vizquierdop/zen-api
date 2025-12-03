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
        public required int Id { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Phone { get; set; }
        public required UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
