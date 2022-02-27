using Catalog.API.Application.Models.DTOs;
using FluentValidation;

namespace Catalog.API.Application.Models.Validators
{
    public class ProductRequestModelValidator : AbstractValidator<CreateProductRequest>
    {
        public ProductRequestModelValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty().WithMessage("Name should not be empty!");
            RuleFor(model => model.Detail)
                .NotEmpty().WithMessage("Details should not be empty!");
            RuleFor(model => model.Price)
                .NotEmpty().WithMessage("Price should not be empty!");
            RuleFor(model => model.IsAvailable)
                .NotEmpty().WithMessage("Is available should not be empty!");
            RuleFor(model => model.IsPopularProduct)
                .NotEmpty().WithMessage("Is popular product should not be empty!");
            RuleFor(model => model.Category)
                .NotEmpty().WithMessage("Category should not be empty!");
            RuleFor(model => model.Rating)
                .NotEmpty().WithMessage("Price should not be empty")
                .GreaterThan(0).WithMessage("Rating should be greater than 0!")
                .LessThan(6).WithMessage("Rating cannot be greater than 5!");
        }
    }
}
