using Catalog.API.Model.Entities;
using MongoDB.Driver;

namespace Catalog.API.DAL.Context
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
