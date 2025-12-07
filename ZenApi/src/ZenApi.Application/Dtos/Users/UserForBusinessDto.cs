using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Mappings;
using ZenApi.Application.Dtos.Businesses;
using ZenApi.Application.Dtos.Provinces;
using ZenApi.Application.Dtos.Reservations;
using ZenApi.Domain.Entities;
using ZenApi.Domain.Enums;

namespace ZenApi.Application.Dtos.Users
{
    public class UserForBusinessDto : IMapFrom<User>
    {
        public required int Id { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
    }
}
