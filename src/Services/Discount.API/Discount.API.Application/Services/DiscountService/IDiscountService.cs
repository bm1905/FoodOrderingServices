using System.Collections.Generic;
using System.Threading.Tasks;
using Discount.API.Application.Models.DTOs.DiscountCoupons;

namespace Discount.API.Application.Services.DiscountService
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountCouponResponse>> ProcessGetDiscountsAsync(string productName);
        Task<bool> ProcessCreateDiscountAsync(CreateDiscountCouponRequest discountCoupon);
        Task<bool> ProcessUpdateDiscountAsync(int couponId, UpdateDiscountCouponRequest discountCoupon);
        Task<bool> ProcessDeleteDiscountAsync(string productName);
    }
}
