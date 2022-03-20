using System.Collections.Generic;
using System.Threading.Tasks;
using Discount.API.Core.Entities;

namespace Discount.API.DataAccess.Repositories
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<DiscountCoupon>> GetDiscountCoupons(string productName);
        Task<bool> CreateDiscountCoupon(DiscountCoupon coupon);
        Task<bool> UpdateDiscountCoupon(int couponId, DiscountCoupon coupon);
        Task<bool> DeleteDiscountCoupon(string productName);
    }
}
