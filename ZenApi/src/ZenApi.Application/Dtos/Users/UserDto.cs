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
    public class UserDto : IMapFrom<User>
    {
        public required int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Phone { get; set; }
        public UserRole Role { get; set; }

        // Relations
        public required int ProvinceId { get; set; }
        public required Province Province { get; set; }

        public IList<Reservation> Reservations { get; set; } = new List<Reservation>();

        public int? BusinessId { get; set; }
        public Business? Business { get; set; }
    }
}
