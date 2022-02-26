using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Model.Entities;

namespace Catalog.API.DAL.Repository
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
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
        Task<ProductPhoto> GetProductPhotoById(string photoId);
        Task AddProductPhoto(ProductPhoto productPhoto);
        Task<bool> SetMainPhoto(string photoId);
        void RemoveProductPhoto(string photoId);
    }
}
