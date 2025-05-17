using AutoMapper;
using eCommerce.Core.Dto;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Mappers
{
    public class RegisterRequestMappingProfile : Profile
    {
        public RegisterRequestMappingProfile()
        {
            CreateMap<RegisterRequest, ApplicationUser>()
                .ForMember(dest => dest.UserID, options => options.Ignore())
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, options => options.MapFrom(src => src.Password))
                .ForMember(dest => dest.PersonName, options => options.MapFrom(src => src.PersonName))
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender));
        }
    }
}
