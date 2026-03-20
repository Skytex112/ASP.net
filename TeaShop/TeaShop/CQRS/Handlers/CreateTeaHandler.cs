using TeaShop.Mapping;
using TeaShop.CQRS.Command;
using TeaShop.Data;
using TeaShop.DTO;
using TeaShop.Models;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace TeaShop.CQRS.Handlers
{
    public class CreateTeaHandler : IRequestHandler<CreateTeaCommand, TeaDto>
    {
        private readonly AppDbContext _context;
        private readonly ISimpleMapper _mapper;

        public CreateTeaHandler(AppDbContext context, ISimpleMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeaDto> Handle(CreateTeaCommand request, CancellationToken cancellationToken)
        {
            var tea = _mapper.MapToTea(request);
            _context.Teas.Add(tea);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.MapToDto(tea);
        }
    }
}
