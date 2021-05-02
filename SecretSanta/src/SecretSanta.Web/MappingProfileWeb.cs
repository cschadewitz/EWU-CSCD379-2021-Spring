using AutoMapper;
using SecretSanta.Api.DTO;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web
{
    public class MappingProfileWeb : Profile
    {
        public MappingProfileWeb()
        {
            CreateMap<UserDTO, UserViewModel>(); //.ForMember(c => c.FullName, options => options.Ignore());
            CreateMap<UserViewModel, UserDTO>(); //.ForSourceMember(c => c.FullName, options => options.DoNotValidate());
        }
    }
}
