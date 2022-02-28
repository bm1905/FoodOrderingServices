using System;
using AutoMapper;
using Catalog.API.Application.Models.DTOs.ProductPhotos;
using Catalog.API.Application.Models.DTOs.Products;
using Catalog.API.Application.Models.Pagination;
using Catalog.API.Core.Entities;

namespace Catalog.API.Application.MappingProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Entities
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>()
                .ForMember(dest => dest.CreatedOn, opt => 
                    opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt =>
                    opt.MapFrom(x => "system"));
            CreateMap<UpdateProductRequest, Product>()
                .ForMember(dest => dest.UpdatedOn, opt =>
                    opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedBy, opt =>
                    opt.MapFrom(x => "update system"));
            CreateMap<ProductPhoto, ProductPhotoResponse>();

            // Pagination
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}