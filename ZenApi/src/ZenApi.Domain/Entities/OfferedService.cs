using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Common;

namespace ZenApi.Domain.Entities
{
    public class OfferedService : BaseAuditableEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? Duration { get; set; }
        public required int Price { get; set; }
        public required bool IsActive { get; set; }

        // Relations
        public required int BusinessId { get; set; }
        public required Business Business { get; set; }

        public virtual IList<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
