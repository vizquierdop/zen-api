using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Mappings;
using ZenApi.Application.Dtos.Availabilities;
using ZenApi.Application.Dtos.Categories;
using ZenApi.Application.Dtos.Holidays;
using ZenApi.Application.Dtos.OfferedServices;
using ZenApi.Application.Dtos.Provinces;
using ZenApi.Application.Dtos.Users;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Dtos.Businesses
{
    public class BusinessDto : IMapFrom<Business>
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Address { get; set; }
        public string? Photo { get; set; }
        public required string Phone { get; set; }
        public int SimultaneousBookings { get; set; }
        public bool IsActive { get; set; }
        public string? Keyword1 { get; set; }
        public string? Keyword2 { get; set; }
        public string? Keyword3 { get; set; }
        public string? InstagramUser { get; set; }
        public string? XUser { get; set; }
        public string? TikTokUser { get; set; }
        public string? FacebookUser { get; set; }
        public string? GoogleMaps { get; set; }
        public int UserId { get; set; }

        public ProvinceDto Province { get; set; } = default!;
        public UserForBusinessDto User { get; set; } = default!;
        public IList<AvailabilityDto> Availabilities { get; set; } = new List<AvailabilityDto>();
        public IList<OfferedServiceDto> OfferedServices { get; set; } = new List<OfferedServiceDto>();
        public IList<HolidayDto> Holidays { get; set; } = new List<HolidayDto>();

        public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Business, BusinessDto>()
                .ForMember(
                    dest => dest.Categories,
                    opt => opt.MapFrom(src => src.BusinessCategories.Select(bc => bc.Category))
                );
                //.ForMember(
                //    dest => dest.Holidays,
                //    opt => opt.MapFrom(src => src.Holidays ?? new List<Holiday>())
                //);
        }
    }
}
