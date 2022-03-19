using System.Threading.Tasks;
using Discount.API.Core.Entities;

namespace Discount.API.DataAccess.Repositories
{
    public interface IDiscountRepository
    {
        Task<DiscountCoupon> GetDiscountCoupon(string productName);
        Task<bool> CreateDiscountCoupon(DiscountCoupon coupon);
        Task<bool> UpdateDiscountCoupon(DiscountCoupon coupon);
        Task<bool> DeleteDiscountCoupon(string productName);
    }
}
