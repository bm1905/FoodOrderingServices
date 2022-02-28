using Catalog.API.Application.Models.DTOs.Products;
using FluentValidation;

namespace Catalog.API.Application.Models.Validators
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(model => model.ProductPhoto)
                .NotEmpty()
                .NotNull()
                .WithMessage("Product photo is missing!");
            RuleFor(model => model.Name)
                .NotEmpty().WithMessage("Name should not be empty!");
            RuleFor(model => model.Detail)
                .NotEmpty().WithMessage("Details should not be empty!");
            RuleFor(model => model.Price)
                .NotEmpty().WithMessage("Price should not be empty!");
            RuleFor(model => model.IsAvailable)
                .NotNull().WithMessage("Is available should not be empty!");
            RuleFor(model => model.IsPopularProduct)
                .NotNull().WithMessage("Is popular product should not be empty!");
            RuleFor(model => model.Category)
                .NotEmpty().WithMessage("Category should not be empty!");
            RuleFor(model => model.Rating)
                .NotEmpty().WithMessage("Price should not be empty")
                .GreaterThan(0).WithMessage("Rating should be greater than 0!")
                .LessThan(6).WithMessage("Rating cannot be greater than 5!");
        }
    }
}
