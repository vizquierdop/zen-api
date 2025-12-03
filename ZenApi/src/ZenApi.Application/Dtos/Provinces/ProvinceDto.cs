using ZenApi.Application.Common.Mappings;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Dtos.Provinces
{
    public class ProvinceDto : IMapFrom<Province>
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
