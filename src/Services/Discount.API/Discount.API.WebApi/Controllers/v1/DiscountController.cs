using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Discount.API.Application.Models.DTOs.DiscountCoupons;
using Discount.API.Application.Services.DiscountService;

namespace Discount.API.WebApi.Controllers.v1
{
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(typeof(DiscountCouponResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DiscountCouponResponse>> GetDiscountCoupon(string productName)
        {
            var discountCoupon = await _discountService.ProcessGetDiscountAsync(productName);
            return Ok(discountCoupon);
        }
    }
}
