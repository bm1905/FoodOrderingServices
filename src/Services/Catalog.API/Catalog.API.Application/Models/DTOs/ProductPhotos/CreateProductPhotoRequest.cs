using Microsoft.AspNetCore.Http;

namespace Catalog.API.Application.Models.DTOs.ProductPhotos
{
    public class CreateProductPhotoRequest
    {
        public string ProductId { get; set; }
        public IFormFile File { get; set; }
    }
}
