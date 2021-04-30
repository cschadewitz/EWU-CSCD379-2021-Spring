using AutoMapper;
using SecretSanta.Data;
using SecretSanta.Api.DTO;

namespace SecretSanta.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
