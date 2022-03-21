using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Discount.API.Core.Entities;
using Discount.API.DataAccess.Persistence;

namespace Discount.API.DataAccess.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDiscountContext _context;

        public DiscountRepository(IDiscountContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }
        public async Task<IEnumerable<DiscountCoupon>> GetDiscountCoupons(string productName)
        {
            const string query = "SELECT * FROM DiscountCoupon WHERE ProductName = @ProductName";

            _context.Connection.Open();
            using var transaction = _context.Connection.BeginTransaction();
            var coupons = await _context.Connection.QueryAsync<DiscountCoupon>(query, new { ProductName = productName }, transaction);
            transaction.Commit();
            _context.Connection.Close();

            return coupons;
        }

        public async Task<IEnumerable<DiscountCoupon>> GetAllDiscountCoupons()
        {
            const string query = "SELECT * FROM DiscountCoupon";

            _context.Connection.Open();
            using var transaction = _context.Connection.BeginTransaction();
            var coupons = await _context.Connection.QueryAsync<DiscountCoupon>(query, transaction: transaction);
            transaction.Commit();
            _context.Connection.Close();

            return coupons;
        }

        public async Task<DiscountCoupon> GetDiscountCouponByCouponCode(string couponCode)
        {
            const string query = "SELECT * FROM DiscountCoupon WHERE CouponCode = @CouponCode";

            _context.Connection.Open();
            using var transaction = _context.Connection.BeginTransaction();
            var coupon = await _context.Connection.QueryFirstOrDefaultAsync<DiscountCoupon>(query, new { CouponCode = couponCode }, transaction);
            transaction.Commit();
            _context.Connection.Close();

            return coupon;
        }

        public async Task<bool> CreateDiscountCoupon(DiscountCoupon coupon)
        {
            const string query = "INSERT INTO DiscountCoupon(CouponCode, ProductName, Description, Amount, ExpiresOn, CreatedBy, CreatedOn) " +
                                 "VALUES(@CouponCode, @ProductName, @Description, @Amount, @ExpiresOn, @CreatedBy, @CreatedOn)";

            _context.Connection.Open();
            using var transaction = _context.Connection.BeginTransaction();
            var affectedRows = await _context.Connection.ExecuteAsync(query,
                new { coupon.CouponCode, coupon.ProductName, coupon.Description, coupon.Amount, coupon.ExpiresOn, coupon.CreatedBy, coupon.CreatedOn },
                transaction);
            transaction.Commit();
            _context.Connection.Close();

            return affectedRows > 0;
        }

        public async Task<bool> UpdateDiscountCoupon(int couponId, DiscountCoupon coupon)
        {
            const string query = "UPDATE DiscountCoupon SET CouponCode=@CouponCode, ProductName=@ProductName, Description = @Description, " +
                                 "Amount = @Amount, ExpiresOn=@ExpiresOn, UpdatedBy=@UpdatedBy, UpdatedOn=@UpdatedOn WHERE Id = @couponId";
            
            _context.Connection.Open();
            using var transaction = _context.Connection.BeginTransaction();
            var affectedRows = await _context.Connection.ExecuteAsync(query,
                new { coupon.CouponCode, coupon.ProductName, coupon.Description, coupon.Amount, coupon.ExpiresOn, coupon.UpdatedOn, coupon.UpdatedBy, couponId },
                transaction);
            transaction.Commit();
            _context.Connection.Close();

            return affectedRows > 0;
        }

        public async Task<bool> DeleteDiscountCoupon(string couponCode)
        {
            const string query = "DELETE FROM DiscountCoupon WHERE CouponCode = @CouponCode";

            _context.Connection.Open();
            using var transaction = _context.Connection.BeginTransaction();
            var affectedRows = await _context.Connection.ExecuteAsync(query,
                new { couponCode },
                transaction);
            transaction.Commit();
            _context.Connection.Close();

            return affectedRows > 0;
        }
    }
}
