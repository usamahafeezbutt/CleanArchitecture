using AutoMapper;
using CleanArchitecture.Application.Services.Customer.Dto;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Mappings
{
    public class CustomerMappingsProfile : Profile
    {
        public CustomerMappingsProfile()
        {
             CreateMap<CustomerDto, ApplicationUser>().ReverseMap();
        }
    }
}
