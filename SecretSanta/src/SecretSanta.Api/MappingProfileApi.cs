using AutoMapper;
using SecretSanta.Data;
using SecretSanta.Api.DTO;

namespace SecretSanta.Api
{
    public partial class MappingProfileApi : Profile
    {
        partial void CustomMaps()
        {
            CreateMap<GroupDTO, Group>();
            CreateMap<Group, GroupDTO>();
        }
    }
}
