using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Common.Models.SearchModels
{
    public record ProvinceSearchModel : BaseQuery
    {
        public string? Name { get; set; }
    }
}
