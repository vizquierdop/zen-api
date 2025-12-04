using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Mappings;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Dtos.Categories
{
    public class CategoryDto : IMapFrom<Category>
    {
        public required int Id { get; set; }
        public string? Name { get; set; }
    }
}
