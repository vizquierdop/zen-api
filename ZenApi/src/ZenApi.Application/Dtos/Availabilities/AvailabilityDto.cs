using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Mappings;
using ZenApi.Application.Dtos.Businesses;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Dtos.Availabilities
{
    public class AvailabilityDto : IMapFrom<Availability>
    {
        public required int Id { get; set; }
        public required int DayOfWeek { get; set; }
        public required string Slot1Start { get; set; }
        public required string Slot1End { get; set; }
        public string? Slot2Start { get; set; }
        public string? Slot2End { get; set; }
        public bool IsActive { get; set; }
        public required int BusinessId { get; set; }
        public required BusinessShortDto Business { get; set; }
    }
}
