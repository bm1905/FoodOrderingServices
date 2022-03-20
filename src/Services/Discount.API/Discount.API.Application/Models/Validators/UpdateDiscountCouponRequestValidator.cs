using Discount.API.Application.Models.DTOs.DiscountCoupons;
using FluentValidation;

namespace Discount.API.Application.Models.Validators
{
    public class UpdateDiscountCouponRequestValidator : AbstractValidator<UpdateDiscountCouponRequest>
    {
        public UpdateDiscountCouponRequestValidator()
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
                .WithMessage("Coupon code must be provided!")
                .MaximumLength(5).WithMessage("Coupon Code must of 5 characters.")
                .MinimumLength(5).WithMessage("Coupon Code must of 5 characters.");
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
