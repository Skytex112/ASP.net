using MediatR;

namespace TeaShop.CQRS.Command
{
    public class DeleteTeaCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
