using TeaShop.DTO;
using MediatR;
namespace TeaShop.CQRS.Command
{
    public class CreateTeaCommand : IRequest<TeaDto>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
