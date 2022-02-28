using Catalog.API.Core.Entities;
using Catalog.API.DataAccess.Persistence.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.DataAccess.Persistence
{
    public class ProductContext : IProductContext
    {
        private IMongoDatabase MongoDatabase { get; set; }
        private MongoClient MongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public ProductContext(IOptions<MongoSettings> configuration)
        {
            MongoClient = new MongoClient(configuration.Value.ConnectionString);
            MongoDatabase = MongoClient.GetDatabase(configuration.Value.DatabaseName);
            Products = MongoDatabase.GetCollection<Product>(configuration.Value.ProductCollectionName);

            //ProductContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
