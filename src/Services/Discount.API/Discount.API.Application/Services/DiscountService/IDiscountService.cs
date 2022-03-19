using System.Threading.Tasks;
using Discount.API.Application.Models.DTOs.DiscountCoupons;

namespace Discount.API.Application.Services.DiscountService
{
    public interface IDiscountService
    {
        Task<DiscountCouponResponse> ProcessGetDiscountAsync(string productName);
        Task<bool> ProcessCreateDiscountAsync(CreateDiscountCouponRequest discountCoupon);
        Task<bool> ProcessUpdateDiscountAsync(UpdateDiscountCouponRequest discountCoupon);
        Task<bool> ProcessDeleteDiscountAsync(string productName);
    }
}
