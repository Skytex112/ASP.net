using AutoMapper;
using Middleware.DTO;
using Middleware.Models;

namespace Middleware.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
