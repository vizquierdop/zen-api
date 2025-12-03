using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Mappings;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Dtos.OfferedServices
{
    public class OfferedServiceDto : IMapFrom<OfferedService>
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public required decimal Price { get; set; }
        public required bool IsActive { get; set; }
        public required int BusinessId { get; set; }
        public required Business Business { get; set; }
    }
}
