using System.Threading.Tasks;
using Catalog.API.Helpers.Pagination;
using Catalog.API.Model.DTOs;

namespace Catalog.API.BLL
{
    public interface IProductServiceBll
    {
        Task<PagedResponse<ProductResponse>> ProcessGetProductsAsync(PaginationQuery paginationQuery);
        Task<ProductResponse> ProcessGetProductByIdAsync(string productId);
        Task<PagedResponse<ProductResponse>> ProcessGetProductsByCategoryAsync(PaginationQuery paginationQuery, string category);
    }
}
