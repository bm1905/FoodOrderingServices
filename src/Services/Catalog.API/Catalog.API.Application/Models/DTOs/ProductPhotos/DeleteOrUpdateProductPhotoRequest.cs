namespace Catalog.API.Application.Models.DTOs.ProductPhotos
{
    public class DeleteOrUpdateProductPhotoRequest
    {
        public string ProductId { get; set; }
        public string ProductPhotoId { get; set; }
    }
}
