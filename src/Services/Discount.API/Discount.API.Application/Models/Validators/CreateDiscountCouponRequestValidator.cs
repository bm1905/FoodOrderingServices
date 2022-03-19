using Discount.API.Application.Models.DTOs.DiscountCoupons;
using FluentValidation;

namespace Discount.API.Application.Models.Validators
{
    public class CreateDiscountCouponRequestValidator : AbstractValidator<CreateDiscountCouponRequest>
    {
        public CreateDiscountCouponRequestValidator()
        {
            RuleFor(model => model.ProductName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Product name should not be empty!");
            RuleFor(model => model.Amount)
                .NotNull()
                .NotEmpty()
                .WithMessage("Discount Amount must be provided!");
            RuleFor(model => model.CouponCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("Coupon code must be provided!");
            RuleFor(model => model.ExpiresIn)
                .NotNull()
                .NotEmpty()
                .WithMessage("Expiration day must be provided!");
            RuleFor(model => model.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Description must be provided!");
        }
    }
}
