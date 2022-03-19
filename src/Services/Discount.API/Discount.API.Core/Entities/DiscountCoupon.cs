using System;
using Discount.API.Core.Common;

namespace Discount.API.Core.Entities
{
    public class DiscountCoupon : BaseEntity, IAuditedEntity
    {
        public string CouponCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public DateTime? ExpiresOn { get; set; }
    }
}
