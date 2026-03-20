using AutoMapper;
using MediatR;
using TeaShop.CQRS.Queries;
using TeaShop.Data;
using TeaShop.DTO;

namespace TeaShop.CQRS.Handlers
{
    public class GetTeaByIdHandler : IRequestHandler<GetTeaByIdQuery, TeaDto>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetTeaByIdHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeaDto> Handle(GetTeaByIdQuery request, CancellationToken cancellationToken)
        {
            var tea = await _context.Teas.FindAsync(request.Id);
            return _mapper.Map<TeaDto>(tea);
        }
    }
}
