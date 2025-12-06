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
    public class CreateBusinessDto : IMapFrom<Business>
    {
        public required string Name { get; set; }
        public string Address { get; set; } = null!;
        public required string Keyword1 { get; set; }
        public required string Keyword2 { get; set; }
        public required string Keyword3 { get; set; }
        public required int ProvinceId { get; set; }
        public bool IsActive { get; set; } = true;
        public int SimultaneousBookings { get; set; } = 1;
        public int[] CategoryIds { get; set; } = Array.Empty<int>();
    }
}
