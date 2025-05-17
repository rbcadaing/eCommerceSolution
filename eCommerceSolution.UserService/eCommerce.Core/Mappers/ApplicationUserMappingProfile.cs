﻿using AutoMapper;
using eCommerce.Core.Dto;
using eCommerce.Core.Entities;
using System.Security.Authentication;

namespace eCommerce.Core.Mappers;

public class ApplicationUserMappingProfile : Profile
{
    public ApplicationUserMappingProfile()
    {
        CreateMap<ApplicationUser, AuthenticationResponse>()
            .ForMember(dest => dest.UserID, options => options.MapFrom(src => src.UserID))
            .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
            .ForMember(dest => dest.PersonName, options => options.MapFrom(src => src.PersonName))
            .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Success, options => options.Ignore())
            .ForMember(dest => dest.Token, options => options.Ignore());
    }
}
