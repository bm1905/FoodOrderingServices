using AutoMapper;
using Catalog.API.Model.Entities;
using Catalog.API.Model.Models;

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