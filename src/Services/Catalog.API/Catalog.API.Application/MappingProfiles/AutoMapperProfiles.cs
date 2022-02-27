using AutoMapper;
using Catalog.API.Application.Models.DTOs;
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
            CreateMap<ProductRequest, Product>();

            // Pagination
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}