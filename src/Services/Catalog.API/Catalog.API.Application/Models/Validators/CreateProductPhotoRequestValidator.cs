using Catalog.API.Application.Models.DTOs.ProductPhotos;
using FluentValidation;

namespace Catalog.API.Application.Models.Validators
{
    public class CreateProductPhotoRequestValidator : AbstractValidator<CreateProductPhotoRequest>
    {
        public CreateProductPhotoRequestValidator()
        {
            RuleFor(model => model.ProductId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Product Id should not be empty!");
            RuleFor(model => model.File)
                .NotNull()
                .NotEmpty()
                .WithMessage("Photo must be provided!");
        }
    }
}
