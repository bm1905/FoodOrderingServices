using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Core.Entities;
using Catalog.API.DataAccess.Persistence;
using MongoDB.Driver;

namespace Catalog.API.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _context;

        public ProductRepository(IProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<long> GetProductsCount()
        {
            return await _context.Products.EstimatedDocumentCountAsync();
        }

        public async Task<IEnumerable<Product>> GetPaginatedProducts(int skip, int pageSize)
        {
            return await _context.Products.Find(p => true).Skip(skip).Limit(pageSize).ToListAsync();
        }
        
        public async Task<Product> GetProductById(string productId)
        {
            return await _context.Products.Find(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByName(string productName)
        {
            return await _context.Products.Find(p => p.Name == productName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetPaginatedProductsByCategory(int skip, int pageSize, string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _context.Products.Find(filter).Skip(skip).Limit(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetPopularProducts()
        {
            return await _context.Products.Find(p => p.IsPopularProduct).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetPaginatedPopularProducts(int skip, int pageSize)
        {
            return await _context.Products.Find(p => p.IsPopularProduct).Skip(skip).Limit(pageSize).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public Task<bool> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductPhoto> GetProductPhotoById(string photoId)
        {
            throw new NotImplementedException();
        }

        public Task AddProductPhoto(ProductPhoto productPhoto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetMainPhoto(string photoId)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductPhoto(string photoId)
        {
            throw new NotImplementedException();
        }
    }
}
