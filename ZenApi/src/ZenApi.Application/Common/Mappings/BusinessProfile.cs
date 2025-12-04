using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Dtos.Businesses;
using ZenApi.Domain.Entities;

namespace ZenApi.Application.Common.Mappings
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<CreateBusinessDto, Business>()
                .ForMember(dest => dest.BusinessCategories, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Availabilities, opt => opt.Ignore());
        }
    }

}
