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
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<ProductPhoto, ProductPhotoDto>().ReverseMap();
        }
    }
}
