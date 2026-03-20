using MediatR;
using TeaShop.CQRS.Command;
using TeaShop.Data;

namespace TeaShop.CQRS.Handlers
{
    public class DeleteTeaHandler : IRequestHandler<DeleteTeaCommand, bool>
    {
        private readonly AppDbContext _context;
        public DeleteTeaHandler(AppDbContext context) { _context = context; }

        public async Task<bool> Handle(DeleteTeaCommand request, CancellationToken cancellationToken)
        {
            var tea = await _context.Teas.FindAsync(request.Id);
            if (tea == null) return false;
            _context.Teas.Remove(tea);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
