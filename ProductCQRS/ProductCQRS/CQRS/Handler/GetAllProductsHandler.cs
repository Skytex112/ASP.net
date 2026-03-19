using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCQRS.CQRS.Query;

using ProductCQRS.Data;
using ProductCQRS.Models;
using ProductCQRS.Profiles;
using Serilog.Core;

namespace ProductCQRS.CQRS.Handler
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, Result<List<ProductViewProfile>>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetAllProductsQueryHandler(AppDbContext appDbContext, IMapper mapper, ILogger logger)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<List<ProductViewProfile>>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _appDbContext.Products.ToListAsync(cancellationToken);
            _logger.LogInformation("Get all products...");
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


