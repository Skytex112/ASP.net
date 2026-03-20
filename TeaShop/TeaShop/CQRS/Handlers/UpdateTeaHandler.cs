using AutoMapper;
using MediatR;
using TeaShop.CQRS.Command;
using TeaShop.Data;
using TeaShop.DTO;

namespace TeaShop.CQRS.Handlers
{
    public class UpdateTeaHandler : IRequestHandler<UpdateTeaCommand, TeaDto>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTeaHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeaDto> Handle(UpdateTeaCommand request, CancellationToken cancellationToken)
        {
            var tea = await _context.Teas.FindAsync(request.Id);
            if (tea == null) return null;

            _mapper.Map(request, tea);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TeaDto>(tea);
        }
    }
}
