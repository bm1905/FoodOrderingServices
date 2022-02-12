using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.DAL.Context;
using Catalog.API.Model.Entities;
using MongoDB.Driver;

namespace Catalog.API.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _context;

        public ProductRepository(IProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public Task<Product> GetProductById(string productId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByName(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetPopularProducts()
        {
            throw new NotImplementedException();
        }

        public Task CreateProduct(Product product)
        {
            throw new NotImplementedException();
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
