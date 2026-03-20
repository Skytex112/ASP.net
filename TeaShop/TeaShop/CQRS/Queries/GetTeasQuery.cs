using TeaShop.DTO;
using TeaShop.Pagination;
using MediatR;

namespace TeaShop.CQRS.Queries
{
    public class GetTeasQuery : IRequest<PagedResult<TeaDto>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
