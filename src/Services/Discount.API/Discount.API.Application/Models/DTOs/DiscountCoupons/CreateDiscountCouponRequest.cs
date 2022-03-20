namespace Discount.API.Application.Models.DTOs.DiscountCoupons
{
    public class CreateDiscountCouponRequest
    {
        public string CouponCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public double ExpiresIn { get; set; }
    }
}
