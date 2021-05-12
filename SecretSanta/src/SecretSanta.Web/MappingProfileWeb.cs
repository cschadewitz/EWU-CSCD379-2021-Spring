using AutoMapper;
using SecretSanta.Api.DTO;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web
{
    public class MappingProfileWeb : Profile
    {
        public MappingProfileWeb()
        {
            CreateMap<UserDTO, UserViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName)); //.ForMember(d => d.FullName, o => o.Ignore());
            CreateMap<UserViewModel, UserDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(src => src.Id))
                .ForMember(d => d.FirstName, o => o.MapFrom(src => src.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(src => src.LastName)); //.ForSourceMember(s => s.FullName, o => o.DoNotValidate());
        }
    }
}
