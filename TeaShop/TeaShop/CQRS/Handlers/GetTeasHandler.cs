using System.Linq;
using TeaShop.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeaShop.CQRS.Queries;
using TeaShop.Data;
using TeaShop.DTO;
using TeaShop.Pagination;

namespace TeaShop.CQRS.Handlers
{
    public class GetTeasHandler : IRequestHandler<GetTeasQuery, PagedResult<TeaDto>>
    {
        private readonly AppDbContext _context;
        private readonly ISimpleMapper _mapper;

        public GetTeasHandler(AppDbContext context, ISimpleMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<TeaDto>> Handle(GetTeasQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Teas.AsQueryable();

            var totalItems = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(t => _mapper.MapToDto(t))
                .ToListAsync(cancellationToken);

            return new PagedResult<TeaDto>
            {
                Items = items,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalItems
            };
        }
    }
}
