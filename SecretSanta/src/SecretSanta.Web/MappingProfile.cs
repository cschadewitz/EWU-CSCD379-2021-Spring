using AutoMapper;
using SecretSanta.Api.DTO;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, UserViewModel>();
            CreateMap<UserViewModel, UserDTO>();
        }
    }
}
