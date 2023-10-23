using AutoMapper;
using ClientManagerDTO.ClientDTO;
using ClientManagerDTO.Entity;

namespace ClientManager.Utilities
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UpdateClientDto, Client>()
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.married, opt => opt.MapFrom(src => src.Married))
                .ForMember(dest => dest.dateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email));

            CreateMap<CreateClientDto, Client>()
                .ForMember(dest => dest.rut, opt => opt.MapFrom(src => src.Rut));

            CreateMap<ClientDto, Client>()
                .ForMember(dest => dest.clientId, opt => opt.MapFrom(src => src.ClientId))
                .ReverseMap();
        }
    }
}
