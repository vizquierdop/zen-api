using AutoMapper;
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
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required int BusinessId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Holiday, HolidayDto>()
               .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
               .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate));
        }
    }
}
