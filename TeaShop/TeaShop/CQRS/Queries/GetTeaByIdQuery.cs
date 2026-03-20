using MediatR;
using TeaShop.DTO;

namespace TeaShop.CQRS.Queries
{
    public class GetTeaByIdQuery : IRequest<TeaDto>
    {
        public Guid Id { get; set; }
    }
}
