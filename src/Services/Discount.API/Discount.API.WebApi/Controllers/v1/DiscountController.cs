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
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DiscountCouponResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<DiscountCouponResponse>>> GetAllDiscountCoupons()
        {
            var discountCoupons = await _discountService.ProcessGetAllDiscountsAsync();
            return Ok(discountCoupons);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{productName}", Name = "GetDiscountCouponByProductName")]
        [ProducesResponseType(typeof(IEnumerable<DiscountCouponResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DiscountCouponResponse>>> GetDiscountCouponByProductName(string productName)
        {
            var discountCoupons = await _discountService.ProcessGetDiscountsAsync(productName);
            return Ok(discountCoupons);
        }

        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(typeof(DiscountCouponResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerErrorException), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<DiscountCouponResponse>> CreateDiscount([FromBody] CreateDiscountCouponRequest discountCoupon)
        {
            await _discountService.ProcessCreateDiscountAsync(discountCoupon);
            return CreatedAtRoute("GetDiscountCouponByProductName", new { productName = discountCoupon.ProductName }, discountCoupon);
        }

        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        [HttpPut("{couponCode:length(5)}")]
        [ProducesResponseType(typeof(DiscountCouponResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DiscountCouponResponse>> UpdateDiscount(string couponCode, [FromBody] UpdateDiscountCouponRequest discountCoupon)
        {
            return Ok(await _discountService.ProcessUpdateDiscountAsync(couponCode, discountCoupon));
        }

        [Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        [HttpDelete("{couponCode:length(5)}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(InternalServerErrorException), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(NotFoundException), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> DeleteDiscount(string couponCode)
        {
            return Ok(await _discountService.ProcessDeleteDiscountAsync(couponCode));
        }
    }
}
