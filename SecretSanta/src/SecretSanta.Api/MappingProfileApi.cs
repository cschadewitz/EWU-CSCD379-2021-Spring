using AutoMapper;
using SecretSanta.Data;
using SecretSanta.Api.DTO;

namespace SecretSanta.Api
{
    public class MappingProfileApi : Profile
    {
        public MappingProfileApi()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
