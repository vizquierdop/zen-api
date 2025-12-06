using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Common;

namespace ZenApi.Domain.Entities
{
    public class Business : BaseAuditableEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Address { get; set; }
        public required int ProvinceId { get; set; }
        public string? Photo { get; set; }
        public string? Keyword1 { get; set; }
        public string? Keyword2 { get; set; }
        public string? Keyword3 { get; set; }
        public required string Phone { get; set; }
        public int SimultaneousBookings { get; set; }
        public bool? IsActive { get; set; }
        public string? InstagramUser { get; set; }
        public string? XUser { get; set; }
        public string? TikTokUser { get; set; }
        public string? FacebookUser { get; set; }
        public string? GoogleMaps { get; set; }
        public int UserId { get; set; }

        // Relations
        public Province Province { get; set; } = null!;
        public User User { get; set; } = null!;
        public virtual IList<Availability> Availabilities { get; set; } = new List<Availability>();
        public virtual IList<OfferedService> OfferedServices { get; set; } = new List<OfferedService>();
        public virtual IList<Holiday> Holidays { get; set; } = new List<Holiday>();
        public virtual IList<BusinessCategory> BusinessCategories{ get; set; } = new List<BusinessCategory>();
    }
}
