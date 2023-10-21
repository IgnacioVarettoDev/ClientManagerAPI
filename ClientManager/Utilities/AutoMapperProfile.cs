using AutoMapper;
using ClientManagerDTO.ClientDTO;
using ClientManagerDTO.Entity;

namespace ClientManager.Utilities
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateClientDTO, Client>();
        }
    }
}
