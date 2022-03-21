using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Discount.API.Application.Exceptions;
using Discount.API.Application.Helpers;
using Discount.API.Application.Models.DTOs.DiscountCoupons;
using Discount.API.Application.Services.AccountService;
using Discount.API.Core.Entities;
using Discount.API.DataAccess.Repositories;

namespace Discount.API.Application.Services.DiscountService
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper, IAccountService accountService)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<IEnumerable<DiscountCouponResponse>> ProcessGetAllDiscountsAsync()
        {
            var discountCoupons = await _discountRepository.GetAllDiscountCoupons();
            if (!discountCoupons.Any())
                throw new NotFoundException($"No any discount coupons found!");

            var mappedDiscountCoupons = _mapper.Map<IEnumerable<DiscountCouponResponse>>(discountCoupons);
            return mappedDiscountCoupons;
        }

        public async Task<IEnumerable<DiscountCouponResponse>> ProcessGetDiscountsAsync(string productName)
        {
            var discountCoupons = (await _discountRepository.GetDiscountCoupons(productName)).ToList();
            if (discountCoupons.Count == 0)
            {
                return new List<DiscountCouponResponse>
                {
                    new()
                    {
                        ProductName = productName,
                        Amount = 0,
                        Description = "No discount found"
                    }
                };
            }

            var mappedDiscountCoupons = _mapper.Map<IEnumerable<DiscountCouponResponse>>(discountCoupons);
            return mappedDiscountCoupons;
        }

        public async Task<bool> ProcessCreateDiscountAsync(CreateDiscountCouponRequest discountCoupon)
        {
            var existingDiscountCoupon = await _discountRepository.GetDiscountCouponByCouponCode(discountCoupon.CouponCode);
            if (existingDiscountCoupon != null) throw new AlreadyExistsException(
                $"Discount for {discountCoupon.ProductName} with code {discountCoupon.CouponCode} already exists!");

            var mappedDiscountCoupon = _mapper.Map<DiscountCoupon>(discountCoupon);
            mappedDiscountCoupon.CreatedBy = AccountHelper.GetUserName(_accountService);

            var isDiscountCreated = await _discountRepository.CreateDiscountCoupon(mappedDiscountCoupon);

            if (!isDiscountCreated) 
                throw new InternalServerErrorException($"Discount {discountCoupon.CouponCode} of {discountCoupon.Amount} for " +
                                                       $"{discountCoupon.ProductName} could not be created!");
            return true;
        }

        public async Task<bool> ProcessUpdateDiscountAsync(string couponCode, UpdateDiscountCouponRequest discountCoupon)
        {
            var existingDiscountCoupon = await _discountRepository.GetDiscountCouponByCouponCode(couponCode);

            if (existingDiscountCoupon == null) throw new AlreadyExistsException(
                $"Discount with code {couponCode} does not exist!");

            var mappedDiscountCoupon = _mapper.Map<DiscountCoupon>(discountCoupon);

            mappedDiscountCoupon.UpdatedBy = AccountHelper.GetUserName(_accountService);
            
            var isDiscountUpdated= await _discountRepository.UpdateDiscountCoupon(existingDiscountCoupon.Id, mappedDiscountCoupon);

            if (!isDiscountUpdated)
                throw new NotFoundException($"Discount {discountCoupon.CouponCode} of {discountCoupon.Amount} for " +
                                                       $"{discountCoupon.ProductName} could not be updated!");
            return true;
        }

        public async Task<bool> ProcessDeleteDiscountAsync(string couponCode)
        {
            var existingDiscountCoupon = await _discountRepository.GetDiscountCouponByCouponCode(couponCode);
            if (existingDiscountCoupon == null)
                throw new NotFoundException($"Discount for {couponCode} does not exist!");

            var isDiscountDeleted = await _discountRepository.DeleteDiscountCoupon(couponCode);

            if (!isDiscountDeleted) throw new InternalServerErrorException($"Discount for {couponCode} not deleted!");

            return true;
        }
    }
}
