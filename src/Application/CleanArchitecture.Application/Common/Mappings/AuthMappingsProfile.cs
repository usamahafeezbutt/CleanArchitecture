

using AutoMapper;
using CleanArchitecture.Application.Services.Account.Dto;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Mappings
{
    public class AuthMappingsProfile : Profile
    {
        public AuthMappingsProfile()
        {
            CreateMap<RegistrationRequestDto, ApplicationUser>()
                .ForMember(destinationMember => destinationMember.UserName, memberOptions => memberOptions.MapFrom(sourceMember => GetUsername(sourceMember)));
        }

        private static string GetUsername(RegistrationRequestDto sourceMember)
        => sourceMember.Email.Split("@").First();
        
    }
}
