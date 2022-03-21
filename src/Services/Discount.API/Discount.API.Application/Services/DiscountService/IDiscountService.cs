using System.Collections.Generic;
using System.Threading.Tasks;
using Discount.API.Application.Models.DTOs.DiscountCoupons;

namespace Discount.API.Application.Services.DiscountService
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountCouponResponse>> ProcessGetDiscountsAsync(string productName);
        Task<IEnumerable<DiscountCouponResponse>> ProcessGetAllDiscountsAsync();
        Task<bool> ProcessCreateDiscountAsync(CreateDiscountCouponRequest discountCoupon);
        Task<bool> ProcessUpdateDiscountAsync(string couponCode, UpdateDiscountCouponRequest discountCoupon);
        Task<bool> ProcessDeleteDiscountAsync(string couponCode);
    }
}
