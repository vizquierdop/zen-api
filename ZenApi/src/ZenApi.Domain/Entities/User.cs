using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Common;
using ZenApi.Domain.Enums;

namespace ZenApi.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool Active { get; set; }

        // Relations
        public required int ProvinceId { get; set; }
        public required Province Province { get; set; }

        public UserRole Role { get; set; }

        public int? BusinessId { get; set; }
        public Business? Business { get; set; }
    }
}
