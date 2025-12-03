using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Mappings;
using ZenApi.Domain.Entities;
using ZenApi.Domain.Enums;

namespace ZenApi.Application.Dtos.Reservations
{
    public class ReservationDto : IMapFrom<Reservation>
    {
        public int Id { get; set; }
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
