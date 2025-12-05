using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Common.Models.SearchModels
{
    public record CategorySearchModel : BaseQuery
    {
        public string? Search { get; set; }
        public string? Name { get; set; }
    }
}
