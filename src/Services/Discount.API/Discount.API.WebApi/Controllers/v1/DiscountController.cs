using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Discount.API.Application.Exceptions;
using Discount.API.Application.Models.DTOs.DiscountCoupons;
using Discount.API.Application.Services.DiscountService;
using Discount.API.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Discount.API.WebApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ValidateModelFilter]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(IEnumerable<DiscountCouponResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DiscountCouponResponse>>> GetDiscountCoupon(string productName)
        {
            var discountCoupon = await _discountService.ProcessGetDiscountsAsync(productName);
            return Ok(discountCoupon);
        }

        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(typeof(DiscountCouponResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerErrorException), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<DiscountCouponResponse>> CreateDiscount([FromBody] CreateDiscountCouponRequest discountCoupon)
        {
            await _discountService.ProcessCreateDiscountAsync(discountCoupon);
            return CreatedAtRoute("GetDiscount", new { productName = discountCoupon.ProductName }, discountCoupon);
        }

        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        [HttpPut("[action]/{couponId:int}")]
        [ProducesResponseType(typeof(DiscountCouponResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DiscountCouponResponse>> UpdateDiscount(int couponId, [FromBody] UpdateDiscountCouponRequest discountCoupon)
        {
            return Ok(await _discountService.ProcessUpdateDiscountAsync(couponId, discountCoupon));
        }

        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerErrorException), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            return Ok(await _discountService.ProcessDeleteDiscountAsync(productName));
        }
    }
}
