using System.Threading.Tasks;
using Catalog.API.Application.Models.DTOs;
using Catalog.API.Application.Models.Pagination;

namespace Catalog.API.Application.Services
{
    public interface IProductServices
    {
        Task<PagedResponse<ProductResponse>> ProcessGetProductsAsync(PaginationQuery paginationQuery);
        Task<ProductResponse> ProcessGetProductByIdAsync(string productId);
        Task<PagedResponse<ProductResponse>> ProcessGetProductsByCategoryAsync(PaginationQuery paginationQuery, string category);
        Task<PagedResponse<ProductResponse>> ProcessGetPopularProductsAsync(PaginationQuery paginationQuery);
        Task<ProductResponse> ProcessCreateProductAsync(CreateProductRequest product);
    }
}
