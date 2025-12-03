using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Mappings;
using ZenApi.Application.Dtos.Categories;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Dtos.Businesses
{
    public class BusinessDto : IMapFrom<Business>
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Address { get; set; }
        public string? Photo { get; set; }
        public required string Phone { get; set; }
        public int SimultaneousBookings { get; set; }
        public bool IsActive { get; set; }
        public string? Keyword1 { get; set; }
        public string? Keyword2 { get; set; }
        public string? Keyword3 { get; set; }
        public string? InstagramUser { get; set; }
        public string? XUser { get; set; }
        public string? TikTokUser { get; set; }
        public string? FacebookUser { get; set; }
        public string? GoogleMaps { get; set; }
        public int UserId { get; set; }

        public required Province Province { get; set; }
        public required User User { get; set; }
        public IList<Availability> Availabilities { get; set; } = new List<Availability>();
        public IList<OfferedService> OfferedServices { get; set; } = new List<OfferedService>();
        public IList<Holiday> Holidays { get; set; } = new List<Holiday>();

        public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
