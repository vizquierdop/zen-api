using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Dtos.Availabilities
{
    public record BulkUpdateAvailabilitiesDto
    {
        public required int Id { get; init; }
        public required int DayOfWeek { get; init; } 
        public required bool IsActive { get; init; }
        public string? Slot1Start { get; init; }
        public string? Slot1End { get; init; }
        public string? Slot2Start { get; init; }
        public string? Slot2End { get; init; }
    }
}
