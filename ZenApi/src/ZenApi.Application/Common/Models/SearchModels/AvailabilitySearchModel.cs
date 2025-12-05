using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Common.Models.SearchModels
{
    public record AvailabilitySearchModel : BaseQuery
    {
        public required int BusinessId { get; set; }
    }
}
