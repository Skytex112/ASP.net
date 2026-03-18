using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCQRS.CQRS.Query;

using ProductCQRS.Data;
using ProductCQRS.Models;
using ProductCQRS.Profiles;

namespace ProductCQRS.CQRS.Handler
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, Result<List<ProductViewProfile>>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<ProductViewProfile>>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _appDbContext.Products.ToListAsync(cancellationToken);
            var result = _mapper.Map<List<ProductViewProfile>>(products);
            return Result<List<ProductViewProfile>>.Success(result);
        }
    }
}

// GetProductByIdQueryRequest
// GetProductByIdQueryHandler
// UpdateProductCommandRequest
// UpdateProductCommandHandler
// DeleteProductCommandRequest
// DeleteProductCommandHandler
// Mapper


