using AutoMapper;
using Domain.Entites.Identity;
using Shared.Dtos.Auth;

namespace Services.Mapping.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
