using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Core.Entities;

namespace Catalog.API.DataAccess.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetPaginatedProducts(int skip, int pageSize);
        Task<long> GetProductsCount();
        Task<Product> GetProductById(string productId);
        Task<Product> GetProductByName(string productName);
        Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);
        Task<IEnumerable<Product>> GetPaginatedProductsByCategory(int skip, int pageSize, string categoryName);
        Task<IEnumerable<Product>> GetPopularProducts();
        Task<IEnumerable<Product>> GetPaginatedPopularProducts(int skip, int pageSize);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
        Task SetMainPhoto(string productPhotoId, string productId);
        Task<bool> RemovePhotoFromProduct(string productPhotoId, string productId);
    }
}
