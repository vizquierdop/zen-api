using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Extensions;

namespace ZenApi.Application.Common.Models
{
    public record BaseQuery
    {
        public int? PaginationSkip { get; set; }
        public int? PaginationLength { get; set; } = 10;
        public string? OrderBy { get; set; }
        public DtOrderDir? OrderDirection { get; set; }
    }
}
