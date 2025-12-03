using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Common;

namespace ZenApi.Domain.Entities
{
    public class Availability : BaseAuditableEntity
    {
        public required int DayOfWeek { get; set; }
        public required string Slot1Start { get; set; }
        public required string Slot1End{ get; set; }
        public string? Slot2Start { get; set; }
        public string? Slot2End{ get; set; }
        public bool IsActive { get; set; } = true;

        // Relations
        public required int BusinessId { get; set; }
        public required Business Business { get; set; }
    }
}
