using Catalog.API.Helpers.Settings;
using Catalog.API.Model.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.DAL.Context
{
    public class ProductContext : IProductContext
    {
        private IMongoDatabase MongoDatabase { get; set; }
        private MongoClient MongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public ProductContext(IOptions<MongoSettings> configuration)
        {
            MongoClient = new MongoClient(configuration.Value.Connection);
            MongoDatabase = MongoClient.GetDatabase(configuration.Value.DatabaseName);
            Products = MongoDatabase.GetCollection<Product>(configuration.Value.CollectionName);

            ProductContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
