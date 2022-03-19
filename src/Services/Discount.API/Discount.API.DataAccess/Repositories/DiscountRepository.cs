using System;
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
        public async Task<DiscountCoupon> GetDiscountCoupon(string productName)
        {
            const string query = "SELECT * FROM Coupon WHERE ProductName = @ProductName";

            using var transaction = _context.Connection.BeginTransaction();
            DiscountCoupon coupon = await _context.Connection.QueryFirstOrDefaultAsync<DiscountCoupon>(query, new { ProductName = productName }, transaction);
            transaction.Commit();

            return coupon;
        }

        public async Task<bool> CreateDiscountCoupon(DiscountCoupon coupon)
        {
            const string query = "INSERT INTO Coupon(CouponCode, ProductName, Description, Amount, ExpiresOn, CreatedBy, CreatedOn) " +
                                 "VALUES(@CouponCode, @ProductName, @Description, @Amount, @ExpiresOn, @CreatedBy, @CreatedOn)";

            using var transaction = _context.Connection.BeginTransaction();
            var affectedRows = await _context.Connection.ExecuteAsync(query,
                new { coupon.CouponCode, coupon.ProductName, coupon.Description, coupon.Amount, coupon.ExpiresOn, coupon.CreatedBy, coupon.CreatedOn },
                transaction);
            transaction.Commit();

            return affectedRows > 0;
        }

        public async Task<bool> UpdateDiscountCoupon(DiscountCoupon coupon)
        {
            const string query = "UPDATE Coupon SET CouponCode=@CouponCode, ProductName=@ProductName, Description = @Description, " +
                                 "Amount = @Amount, ExpiresOn=@ExpiresOn, UpdatedBy=@UpdatedBy, UpdatedOn=@UpdatedOn WHERE Id = @Id";

            using var transaction = _context.Connection.BeginTransaction();
            var affectedRows = await _context.Connection.ExecuteAsync(query,
                new { coupon.CouponCode, coupon.ProductName, coupon.Description, coupon.Amount, coupon.ExpiresOn, coupon.UpdatedOn, coupon.UpdatedBy, coupon.Id },
                transaction);
            transaction.Commit();

            return affectedRows > 0;
        }

        public async Task<bool> DeleteDiscountCoupon(string productName)
        {
            const string query = "DELETE FROM Coupon WHERE ProductName = @ProductName";

            using var transaction = _context.Connection.BeginTransaction();
            var affectedRows = await _context.Connection.ExecuteAsync(query,
                new { productName },
                transaction);
            transaction.Commit();

            return affectedRows > 0;
        }
    }
}
