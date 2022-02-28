using System.Threading.Tasks;
using Catalog.API.Application.Models.DTOs;
using Catalog.API.Application.Models.DTOs.ProductPhotos;
using Catalog.API.Application.Models.DTOs.Products;
using Catalog.API.Application.Models.Pagination;

namespace Catalog.API.Application.Services.ProductService
{
    public interface IProductServices
    {
        Task<PagedResponse<ProductResponse>> ProcessGetProductsAsync(PaginationQuery paginationQuery);
        Task<ProductResponse> ProcessGetProductByIdAsync(string productId);
        Task<PagedResponse<ProductResponse>> ProcessGetProductsByCategoryAsync(PaginationQuery paginationQuery, string category);
        Task<PagedResponse<ProductResponse>> ProcessGetPopularProductsAsync(PaginationQuery paginationQuery);
        Task<ProductResponse> ProcessCreateProductAsync(CreateProductRequest product);
        Task<ProductResponse> ProcessUpdateProductAsync(string productId, UpdateProductRequest product);
        Task<ProductPhotoResponse> ProcessAddPhotoAsync(CreateProductPhotoRequest photoRequest);
        Task ProcessDeleteProductByIdAsync(string productId);
        Task ProcessDeleteProductPhotoByIdAsync(DeleteOrUpdateProductPhotoRequest deleteOrUpdateProductPhotoRequest);
        Task ProcessSetMainPhotoByIdAsync(DeleteOrUpdateProductPhotoRequest deleteOrUpdateProductPhotoRequest);
    }
}
