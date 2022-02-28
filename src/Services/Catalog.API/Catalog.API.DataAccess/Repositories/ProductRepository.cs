using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> UpdateProduct(Product product)
        {
            ReplaceOneResult updateResult =
                await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task SetMainPhoto(string productPhotoId, string productId)
        {
            // Find the main photo and set it to false
            await _context.Products.FindOneAndUpdateAsync(
                c => c.Id == productId && c.ProductPhotos.Any(s => s.IsMain == true),
                Builders<Product>.Update.Set(c => c.ProductPhotos[-1].IsMain, false));

            // Set given photo to be main
            await _context.Products.FindOneAndUpdateAsync(
                c => c.Id == productId && c.ProductPhotos.Any(s => s.Id == productPhotoId),
                Builders<Product>.Update.Set(c => c.ProductPhotos[-1].IsMain, true));
        }

        public async Task<bool> RemovePhotoFromProduct(string productPhotoId, string productId)
        {
            var filter = Builders<Product>.Filter.Where(x => x.Id == productId);
            var update = Builders<Product>.Update.PullFilter(x => x.ProductPhotos, 
                Builders<ProductPhoto>.Filter.Where(p => p.Id == productPhotoId));
            var updateResult = await _context.Products.UpdateOneAsync(filter, update);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
