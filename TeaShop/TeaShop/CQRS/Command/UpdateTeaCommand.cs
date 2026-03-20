using MediatR;
using TeaShop.DTO;

namespace TeaShop.CQRS.Command
{
    public class UpdateTeaCommand : IRequest<TeaDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
