using System;
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

        public async Task<DiscountCouponResponse> ProcessGetDiscountAsync(string productName)
        {
            var discountCoupon = await _discountRepository.GetDiscountCoupon(productName);
            if (discountCoupon == null)
            {
                return new DiscountCouponResponse
                {
                    ProductName = productName,
                    Amount = 0,
                    Description = "No discount found"
                };
            }

            var mappedDiscountCoupon = _mapper.Map<DiscountCouponResponse>(discountCoupon);
            return mappedDiscountCoupon;
        }

        public async Task<bool> ProcessCreateDiscountAsync(CreateDiscountCouponRequest discountCoupon)
        {
            var mappedDiscountCoupon = _mapper.Map<DiscountCoupon>(discountCoupon);
            mappedDiscountCoupon.CreatedBy = "system";

            var isDiscountCreated = await _discountRepository.CreateDiscountCoupon(mappedDiscountCoupon);

            if (!isDiscountCreated) 
                throw new InternalServerErrorException($"Discount {discountCoupon.CouponCode} of {discountCoupon.Amount} for " +
                                                       $"{discountCoupon.ProductName} could not be created!");
            return true;
        }

        public async Task<bool> ProcessUpdateDiscountAsync(UpdateDiscountCouponRequest discountCoupon)
        {
            var existingDiscountCoupon = await _discountRepository.GetDiscountCoupon(discountCoupon.ProductName);
            if (existingDiscountCoupon == null)
                throw new NotFoundException($"Discount for {discountCoupon.ProductName} does not exist!");

            var mappedDiscountCoupon = _mapper.Map<DiscountCoupon>(discountCoupon);
            mappedDiscountCoupon.UpdatedBy = "update system";
            
            var isDiscountUpdated= await _discountRepository.UpdateDiscountCoupon(mappedDiscountCoupon);

            if (!isDiscountUpdated)
                throw new InternalServerErrorException($"Discount {discountCoupon.CouponCode} of {discountCoupon.Amount} for " +
                                                       $"{discountCoupon.ProductName} could not be updated!");
            return true;
        }

        public async Task<bool> ProcessDeleteDiscountAsync(string productName)
        {
            var existingDiscountCoupon = await _discountRepository.GetDiscountCoupon(productName);
            if (existingDiscountCoupon == null)
                throw new NotFoundException($"Discount for {productName} does not exist!");

            var isDiscountDeleted = await _discountRepository.DeleteDiscountCoupon(productName);

            if (!isDiscountDeleted) throw new InternalServerErrorException($"Discount for {productName} not deleted!");

            return true;
        }
    }
}
