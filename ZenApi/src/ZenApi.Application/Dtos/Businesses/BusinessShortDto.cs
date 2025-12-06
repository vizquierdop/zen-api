using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Mappings;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Dtos.Businesses
{
    public class BusinessShortDto : IMapFrom<Business>
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Address { get; set; }
    }
}
