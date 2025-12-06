using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Common.Models.SearchModels
{
    public record ReservationSearchModel : BaseQuery
    {
        public int? UserId { get; set; }
        public int? BusinessId { get; set; }
        public string? Search { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? StatusTypes { get; set; }
        public string? ServiceIds { get; set; }
    }
}
