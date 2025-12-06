using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Mappings;
using ZenApi.Application.Dtos.Businesses;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Dtos.Holidays
{
    public class HolidayDto : IMapFrom<Holiday>
    {
        public required int Id { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required int BusinessId { get; set; }
    }
}
