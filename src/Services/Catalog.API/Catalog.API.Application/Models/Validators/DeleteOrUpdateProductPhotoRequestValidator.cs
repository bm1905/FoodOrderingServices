using Catalog.API.Application.Models.DTOs.ProductPhotos;
using FluentValidation;

namespace Catalog.API.Application.Models.Validators
{
    public class DeleteOrUpdateProductPhotoRequestValidator : AbstractValidator<DeleteOrUpdateProductPhotoRequest>
    {
        public DeleteOrUpdateProductPhotoRequestValidator()
        {
            RuleFor(model => model.ProductId)
                .NotEmpty().WithMessage("Product Id is required!");
            RuleFor(model => model.ProductPhotoId)
                .NotEmpty().WithMessage("Photo Id is required!");
        }
    }
}
