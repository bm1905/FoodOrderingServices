using AutoMapper;
using Catalog.API.Model.DTOs;
using Catalog.API.Model.Entities;

namespace Catalog.API.Helpers.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductResponse>();
        }
    }
}