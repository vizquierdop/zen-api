using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Common;
using ZenApi.Domain.Enums;

namespace ZenApi.Domain.Entities
{
    public class Reservation : BaseAuditableEntity
    {
        public required DateTime Date { get; set; }
        public required string StartTime { get; set; }
        public required string EndTime { get; set; }
        public required ReservationStatus Status { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }

        // Relations
        public required int ServiceId { get; set; }
        public required OfferedService Service { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
