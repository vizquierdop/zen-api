using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Common.Models.SearchModels
{
    public record BusinessSearchModel : BaseQuery
    {
        public string? CategoryIds { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public int? ProvinceId { get; set; }
    }
}
