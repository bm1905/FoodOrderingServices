using AutoMapper;
using Catalog.API.Helpers.Pagination;
using Catalog.API.Model.DTOs;
using Catalog.API.Model.Entities;

namespace Catalog.API.Helpers.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Entities
            CreateMap<Product, ProductResponse>();

            // Pagination
            CreateMap<PaginationQuery, PaginationFilter>();
        }
    }
}