using AutoMapper;
using TeaShop.CQRS.Command;
using TeaShop.DTO;
using TeaShop.Models;

namespace TeaShop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tea, TeaDto>().ReverseMap();
            CreateMap<CreateTeaCommand, TeaDto>();
            CreateMap<UpdateTeaCommand, TeaDto>();
        }
    }
}
