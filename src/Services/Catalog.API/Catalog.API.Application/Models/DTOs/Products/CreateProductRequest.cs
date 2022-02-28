using Microsoft.AspNetCore.Http;

namespace Catalog.API.Application.Models.DTOs.Products
{
    public class CreateProductRequest : ProductRequest
    {
        public IFormFile ProductPhoto { get; set; }
    }
}
