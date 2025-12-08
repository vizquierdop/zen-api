using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Common.Models.SearchModels
{
    public record OfferedServiceSearchModel : BaseQuery
    {
        public int? BusinessId { get; set; }
        public string? Search { get; set; }
        public string? Name { get; set; }
        public int? Duration { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
        public int? ProvinceId { get; set; }
    }
}
