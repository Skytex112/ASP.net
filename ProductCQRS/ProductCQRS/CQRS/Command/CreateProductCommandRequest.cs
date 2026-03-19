using FluentValidation;
using ProductCQRS.Models;
using MediatR;
using ProductCQRS.Profiles;

namespace ProductCQRS.CQRS.Command
{
    public class GetAllProductsQueryRequest : IRequest<Result<ProductViewProfile>>
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string Code { get; set; }
        public Guid? CategoryId { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateProductValidator : AbstractValidator<GetAllProductsQueryRequest>
    {
        public CreateProductValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name cannot be empty");
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater then 0");
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code name cannot be empty")
                .Matches(@"^\d{13}")
                .WithMessage("Barcode must contain exactly 13 digits");
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("CategoryId name cannot be empty");
            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 100)
                .WithMessage("Price must be greater then 0 and less then 100");
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater then 0");
        }
    }
}
