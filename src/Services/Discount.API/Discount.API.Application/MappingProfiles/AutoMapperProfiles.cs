using System;
using AutoMapper;
using Discount.API.Application.Models.DTOs.DiscountCoupons;
using Discount.API.Core.Entities;

namespace Discount.API.Application.MappingProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Entities
            CreateMap<DiscountCoupon, DiscountCouponResponse>();
            CreateMap<CreateDiscountCouponRequest, DiscountCoupon>()
                .ForMember(dest => dest.CreatedOn, opt => 
                    opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.ExpiresOn, opt => 
                    opt.MapFrom(x => DateTime.UtcNow.AddDays(x.ExpiresIn)));
            CreateMap<UpdateDiscountCouponRequest, DiscountCoupon>()
                .ForMember(dest => dest.UpdatedOn, opt =>
                    opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.ExpiresOn, opt =>
                    opt.MapFrom(x => DateTime.UtcNow.AddDays(x.ExpiresIn)));
        }
    }
}