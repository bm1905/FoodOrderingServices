using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Discount.API.Application.Exceptions;
using Discount.API.Application.Models.DTOs.DiscountCoupons;
using Discount.API.Core.Entities;
using Discount.API.DataAccess.Repositories;

namespace Discount.API.Application.Services.DiscountService
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            var existingDiscountCoupons = (await _discountRepository.GetDiscountCoupons(discountCoupon.ProductName)).ToList();
            if (existingDiscountCoupons.Count > 0)
            {
                foreach (var existingDiscountCoupon in existingDiscountCoupons)
                {
                    if (existingDiscountCoupon.ProductName == discountCoupon.ProductName &&
                        existingDiscountCoupon.CouponCode == discountCoupon.CouponCode)
                    {
                        throw new AlreadyExistsException(
                            $"Discount for {discountCoupon.ProductName} with code {discountCoupon.CouponCode} already exists!");
                    }
                }
            }
            var mappedDiscountCoupon = _mapper.Map<DiscountCoupon>(discountCoupon);
            mappedDiscountCoupon.CreatedBy = "system";

            var isDiscountCreated = await _discountRepository.CreateDiscountCoupon(mappedDiscountCoupon);

            if (!isDiscountCreated) 
                throw new InternalServerErrorException($"Discount {discountCoupon.CouponCode} of {discountCoupon.Amount} for " +
                                                       $"{discountCoupon.ProductName} could not be created!");
            return true;
        }

        public async Task<bool> ProcessUpdateDiscountAsync(int couponId, UpdateDiscountCouponRequest discountCoupon)
        {
            var mappedDiscountCoupon = _mapper.Map<DiscountCoupon>(discountCoupon);

            mappedDiscountCoupon.UpdatedBy = "update system";
            
            var isDiscountUpdated= await _discountRepository.UpdateDiscountCoupon(couponId, mappedDiscountCoupon);

            if (!isDiscountUpdated)
                throw new NotFoundException($"Discount {discountCoupon.CouponCode} of {discountCoupon.Amount} for " +
                                                       $"{discountCoupon.ProductName} could not be updated!");
            return true;
        }

        public async Task<bool> ProcessDeleteDiscountAsync(string productName)
        {
            var existingDiscountCoupon = await _discountRepository.GetDiscountCoupons(productName);
            if (existingDiscountCoupon == null)
                throw new NotFoundException($"Discount for {productName} does not exist!");

            var isDiscountDeleted = await _discountRepository.DeleteDiscountCoupon(productName);

            if (!isDiscountDeleted) throw new InternalServerErrorException($"Discount for {productName} not deleted!");

            return true;
        }
    }
}
